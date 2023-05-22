using Dapper;
using Microsoft.Extensions.Configuration;
using MSIA.WebFresher032023.Demo.DL_Repositories.Entity;
using System.Data;

namespace MSIA.WebFresher032023.Demo.DL_Repositories.Repositories.Empl
{
    public class EmployeeRepository : BaseRepository<Employee> ,IEmployeeRepository
    {
        public EmployeeRepository(IConfiguration configuration) : base(configuration)
        {
        }

        public override async Task<bool> PostAsync(Employee employee)
        {
            var connection = await GetOpenConnectionAsync();
            var parameters = new
            {
                p_EmployeeCode = employee.EmployeeCode,
                p_FullName = employee.FullName,
                p_Gender = employee.Gender,
                p_DateOfBirth = employee.DateOfBirth,
                p_Email = employee.Email,
                p_Mobile = employee.Mobile,
                p_DepartmentId = employee.DepartmentId,
                p_CreatedDate = DateTime.Now,
                p_CreatedBy = employee.CreatedBy,
                p_ModifiedDate = DateTime.Now,
                p_ModifiedBy = employee.ModifiedBy
            };
            var affectedRow = await connection.ExecuteAsync("Proc_InsertEmployee", parameters, commandType: CommandType.StoredProcedure);
            await connection.CloseAsync();
            return affectedRow > 0;
        }

        public override async Task<bool> PutAsync(Guid employeeId, Employee employee)
        {
            var connection = await GetOpenConnectionAsync();
            var parameters = new
            {
                p_EmployeeId = employeeId,
                p_EmployeeCode = employee.EmployeeCode,
                p_FullName = employee.FullName,
                p_Gender = employee.Gender,
                p_DateOfBirth = employee.DateOfBirth,
                p_Email = employee.Email,
                p_Mobile = employee.Mobile,
                p_DepartmentId = employee.DepartmentId,
                p_ModifiedDate = DateTime.Now,
                p_ModifiedBy = employee.ModifiedBy
            };
            var affectedRow = await connection.ExecuteAsync("Proc_UpdateEmployee", parameters, commandType: CommandType.StoredProcedure);
            await connection.CloseAsync();
            return affectedRow > 0;
        }
    }
}
