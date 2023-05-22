using Dapper;
using Microsoft.Extensions.Configuration;
using MSIA.WebFresher032023.Demo.DL_Repositories.Entity;
using System.Data;

namespace MSIA.WebFresher032023.Demo.DL_Repositories.Repositories.Depart
{
    public class DepartmentRepository : BaseRepository<Department> ,IDepartmentRepository
    {
        public DepartmentRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public async Task<bool> PostAsync(Department department)
        {
            var connection = await GetOpenConnectionAsync();
            var parameters = new
            {
                p_DepartmentName = department.DepartmentName,
                p_CreatedDate = DateTime.Now,
                p_CreatedBy = department.CreatedBy,
                p_ModifiedDate = DateTime.Now,
                p_ModifiedBy = department.ModifiedBy
            };
            var affectedRow = await connection.ExecuteAsync("Proc_InsertDepartment", parameters, commandType: CommandType.StoredProcedure);
            await connection.CloseAsync();
            return affectedRow > 0;
        }

        public async Task<bool> PutAsync(Guid departmentId, Department department)
        {
            var connection = await GetOpenConnectionAsync();
            var parameters = new
            {
                p_DepartmentId = departmentId,
                p_DepartmentName = department.DepartmentName,
                p_ModifiedBy = department.ModifiedBy
            };
            var affectedRow = await connection.ExecuteAsync("Proc_UpdateDepartment", parameters, commandType: CommandType.StoredProcedure);
            await connection.CloseAsync();
            return affectedRow > 0;
        }
    }
}
