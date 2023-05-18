using Dapper;
using Microsoft.AspNetCore.Mvc;
using MSIA.WebFresher032023.ConnectToDB.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace MSIA.WebFresher032023.ConnectToDB.Repositories
{
    /// <summary>
    /// Class thực hiện các thao tác liên quan đến lưu trữ và truy vấn dữ liệu
    /// cho đối tượng Department
    /// </summary>
    /// /// <remarks>
    /// Đối tượng DepartmentRepository cung cấp các phương thức để truy xuất, thêm mới, 
    /// cập nhật và xóa các phòng ban trong cơ sở dữ liệu.
    /// Các phương thức này sử dụng kết nối đến cơ sở dữ liệu thông qua IDbConnection và 
    /// sử dụng các thủ tục lưu trữ (stored procedures)
    /// để thực hiện các thao tác tương ứng.
    /// </remarks>
    /// Created by: ldtuan (17/05/2023)
    public class DepartmentRepository
    {
        private readonly IConfiguration _config;
        public DepartmentRepository(IConfiguration config)
        {
            _config = config;
        }
        /// <summary>
        /// Lấy 1 đối tượng kết nối đến cơ sở dữ liệu.
        /// </summary>
        /// <returns>IDbConnection: sử dụng để thực hiện các thao tác đến cơ sở dũ liệu.</returns>
        /// Created by: ldtuan (17/05/2023)
        private IDbConnection GetConnection()
        {
            string connectionString = _config.GetConnectionString("MariadbConnection");
            return new MySqlConnection(connectionString);
        }
        /// <summary>
        /// Lấy danh sách tất cả phòng ban
        /// </summary>
        /// <returns>Danh sách phòng ban.</returns>
        /// <remarks>
        /// Phương thức này sử dụng thủ tục lưu trữ Proc_GetListDepartments để truy vấn danh sách các phòng ban từ cơ sở dữ liệu.
        /// </remarks>
        /// Created by: ldtuan (17/05/2023)
        public async Task<IEnumerable<Department>> GetListDepartments()
        {
            using (IDbConnection conn = GetConnection())
            {
                conn.Open();
                return await conn.QueryAsync<Department>("Proc_GetListDepartments", commandType:CommandType.StoredProcedure);
            }
        }
        /// <summary>
        /// Lấy thông tin phòng ban theo id
        /// </summary>
        /// <param name="id">Id do người dùng nhập vào.</param>
        /// <returns>1 phòng ban có id tương ứng.</returns>
        /// <remarks>
        /// Phương thức này sử dụng thủ tục lưu trữ Proc_GetDepartmentById để truy vấn phòng ban theo id từ cơ sở dữ liệu.
        /// </remarks>
        /// Created by: ldtuan (17/05/2023)
        public async Task<Department> GetDepartmentById(Guid id)
        {
            using (IDbConnection conn = GetConnection())
            {
                conn.Open();
                return await conn.QueryFirstOrDefaultAsync<Department>("Proc_GetDepartmentById", param: new { p_DepartmenId = id}, commandType:CommandType.StoredProcedure);
            }
        }
        /// <summary>
        /// Thêm mới phòng ban
        /// </summary>
        /// <param name="department">Thông tin phòng ban do người dùng nhập vào.</param>
        /// <returns>Kết quả thêm mới phòng ban.</returns>
        /// <remarks>
        /// Phương thức này sử dụng thủ tục lưu trữ Proc_InsertDepartment để thêm mới một phòng ban vào cơ sở dữ liệu.
        /// Sử dụng phương thức ExecuteAsync của IDbConnection để thực hiện thủ tục lưu trữ và trả về số dòng bị ảnh hưởng.
        /// Trả về true nếu số dòng bị ảnh hưởng lớn hơn 0, ngược lại trả về false.
        /// </remarks>
        /// Created by: ldtuan (17/05/2023)
        public async Task<bool> InsertDepartment(Department department)
        {
            using (IDbConnection conn = GetConnection())
            {
                conn.Open();
                var parameters = new
                {
                    p_DepartmentName = department.DepartmentName,
                    p_CreatedDate = DateTime.Now,
                    p_CreatedBy = department.CreatedBy,
                    p_ModifiedDate = DateTime.Now,
                    p_ModifiedBy = department.ModifiedBy
                };
                var affectedRow = await conn.ExecuteAsync("Proc_InsertDepartment", parameters, commandType: CommandType.StoredProcedure);
                return affectedRow > 0;
            }
        }
        /// <summary>
        /// Cập nhật thông tin phòng ban
        /// </summary>
        /// <param name="id">Id của phòng ban do người dùng chọn để cập nhật.</param>
        /// <param name="department">Thông tin phòng ban do người dùng nhập vào.</param>
        /// <returns>Kết quả cập nhật phòng ban.</returns>
        /// <remarks>
        /// Phương thức này sử dụng thủ tục lưu trữ Proc_UpdateDepartment để cập nhật thông tin phòng ban trong cơ sở dữ liệu.
        /// Sử dụng phương thức ExecuteAsync của IDbConnection để thực hiện thủ tục lưu trữ và trả về số dòng bị ảnh hưởng.
        /// Trả về true nếu số dòng bị ảnh hưởng lớn hơn 0, ngược lại trả về false.
        /// </remarks>
        /// Created by: ldtuan (17/05/2023)
        public async Task<bool> UpdateDepartment(Guid id, Department department)
        {
            using (IDbConnection conn = GetConnection())
            {
                conn.Open();
                var parameters = new
                {
                    p_DepartmentId = id,
                    p_DepartmentName = department.DepartmentName,
                    p_ModifiedBy = department.ModifiedBy
                };
                var affectedRow = await conn.ExecuteAsync("Proc_UpdateDepartment", parameters, commandType: CommandType.StoredProcedure);
                return affectedRow > 0;
            }
        }
        /// <summary>
        /// Xóa 1 phòng ban
        /// </summary>
        /// <param name="id">Id của phòng ban mà người dùng chọn để xóa.</param>
        /// <returns>Kết quả xóa phòng ban.</returns>
        /// <remarks>
        /// Phương thức này sử dụng thủ tục lưu trữ Proc_DeleteDepartmentById để xóa một phòng ban khỏi cơ sở dữ liệu.
        /// Sử dụng phương thức ExecuteAsync của IDbConnection để thực hiện thủ tục lưu trữ và trả về số dòng bị ảnh hưởng.
        /// Trả về true nếu số dòng bị ảnh hưởng lớn hơn 0, ngược lại trả về false.
        /// </remarks>
        /// Created by: ldtuan (17/05/2023)
        public async Task<bool> DeleteDepartment(Guid id)
        {
            using (IDbConnection conn = GetConnection())
            {
                conn.Open();
                var parameters = new
                {
                    p_DepartmentId = id
                };
                var affectedRow = await conn.ExecuteAsync("Proc_DeleteDepartmentById", parameters, commandType: CommandType.StoredProcedure);
                return affectedRow > 0;
            }
        }
        /// <summary>
        /// Lọc và phân trang các phòng ban
        /// </summary>
        /// <param name="pageNumber">Số trang mà người dùng đang ở.</param>
        /// <param name="pageLimit">Số lượng tối đa phòng ban mà 1 trang có thể hiển thị.</param>
        /// <param name="filterName">Tên phòng ban để lọc, có thể null.</param>
        /// <returns>Danh sách phòng ban được phân trang và lọc.</returns>
        /// <remarks>
        /// Phương thức này sử dụng thủ tục lưu trữ Proc_FilterDepartment để lọc và phân trang các phòng ban trong cơ sở dữ liệu.
        /// Nếu không có tên phòng ban thì sẽ phân trang theo danh sách tất cả các phòng ban.
        /// </remarks>
        /// Created by: ldtuan (17/05/2023)
        public async Task<IEnumerable<Department>> FilterAndPagingDepartments(int pageNumber, int pageLimit, string filterName)
        {
            using (IDbConnection conn = GetConnection())
            {
                conn.Open();
                var parameters = new
                {
                    p_PageNumber = pageNumber,
                    p_PageLimit = pageLimit,
                    p_FilterName = filterName
                };
                return await conn.QueryAsync<Department>("Proc_FilterDepartment", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
