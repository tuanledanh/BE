using Dapper;
using Microsoft.AspNetCore.Mvc;
using MSIA.WebFresher032023.ConnectToDB.EnumExtension;
using MSIA.WebFresher032023.ConnectToDB.Mapper.DTO;
using MSIA.WebFresher032023.ConnectToDB.Models;
using MySql.Data.MySqlClient;
using System.Data;

namespace MSIA.WebFresher032023.ConnectToDB.Repositories
{
    /// <summary>
    /// Class thực hiện các thao tác liên quan đến lưu trữ và truy vấn dữ liệu
    /// cho đối tượng Employee
    /// </summary>
    /// /// <remarks>
    /// Đối tượng EmployeeRepository cung cấp các phương thức để truy xuất, thêm mới, 
    /// cập nhật và xóa các nhân viên trong cơ sở dữ liệu.
    /// Các phương thức này sử dụng kết nối đến cơ sở dữ liệu thông qua IDbConnection và 
    /// sử dụng các thủ tục lưu trữ (stored procedures)
    /// để thực hiện các thao tác tương ứng.
    /// </remarks>
    /// Created by: ldtuan (17/05/2023)
    public class EmployeeRepository
    {
        private readonly IConfiguration _config;
        public EmployeeRepository(IConfiguration config)
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
        /// Lấy danh sách tất cả nhân viên
        /// </summary>
        /// <returns>Danh sách nhân viên.</returns>
        /// <remarks>
        /// Phương thức này sử dụng thủ tục lưu trữ Proc_GetListEmployees để truy vấn danh sách các nhân viên từ cơ sở dữ liệu.
        /// </remarks>
        /// Created by: ldtuan (17/05/2023)
        public async Task<IEnumerable<Employee>> GetListEmployees()
        {
            using (IDbConnection conn = GetConnection())
            {
                conn.Open();
                return await conn.QueryAsync<Employee>("Proc_GetListEmployees", commandType: CommandType.StoredProcedure);
            }
        }
        /// <summary>
        /// Lấy thông tin nhân viên theo id
        /// </summary>
        /// <param name="id">Id do người dùng nhập vào.</param>
        /// <returns>1 nhân viên có id tương ứng.</returns>
        /// <remarks>
        /// Phương thức này sử dụng thủ tục lưu trữ Proc_GetEmployeeById để truy vấn nhân viên theo id từ cơ sở dữ liệu.
        /// </remarks>
        /// Created by: ldtuan (17/05/2023)
        public async Task<Employee> GetEmployeeById(Guid id)
        {
            using (IDbConnection conn = GetConnection())
            {
                conn.Open();
                return await conn.QueryFirstOrDefaultAsync<Employee>("Proc_GetEmployeeById", param: new { p_EmployeeId = id }, commandType: CommandType.StoredProcedure);
            }
        }
        /// <summary>
        /// Thêm mới nhân viên
        /// </summary>
        /// <param name="Employee">Thông tin nhân viên do người dùng nhập vào.</param>
        /// <returns>Kết quả thêm mới nhân viên.</returns>
        /// <remarks>
        /// Phương thức này sử dụng thủ tục lưu trữ Proc_InsertEmployee để thêm mới một nhân viên vào cơ sở dữ liệu.
        /// Sử dụng phương thức ExecuteAsync của IDbConnection để thực hiện thủ tục lưu trữ và trả về số dòng bị ảnh hưởng.
        /// Trả về true nếu số dòng bị ảnh hưởng lớn hơn 0, ngược lại trả về false.
        /// </remarks>
        /// Created by: ldtuan (17/05/2023)
        public async Task<bool> InsertEmployee(EmployeeDto employeeDto)
        {
            using (IDbConnection conn = GetConnection())
            {
                conn.Open();
                var parameters = new
                {
                    p_EmployeeCode = employeeDto.EmployeeCode,
                    p_FullName = employeeDto.FullName,
                    p_Gender = GenderExtension.ConvertStringGender(employeeDto.Gender),
                    p_DateOfBirth = employeeDto.DateOfBirth,
                    p_Email = employeeDto.Email,
                    p_Mobile = employeeDto.Mobile,
                    p_DepartmentId = employeeDto.DepartmentId,
                    p_CreatedDate = DateTime.Now,
                    p_CreatedBy = employeeDto.CreatedBy,
                    p_ModifiedDate = DateTime.Now,
                    p_ModifiedBy = employeeDto.ModifiedBy
                };
                var affectedRow = await conn.ExecuteAsync("Proc_InsertEmployee", parameters, commandType: CommandType.StoredProcedure);
                return affectedRow > 0;
            }
        }
        /// <summary>
        /// Cập nhật thông tin nhân viên
        /// </summary>
        /// <param name="id">Id của nhân viên do người dùng chọn để cập nhật.</param>
        /// <param name="Employee">Thông tin nhân viên do người dùng nhập vào.</param>
        /// <returns>Kết quả cập nhật nhân viên.</returns>
        /// <remarks>
        /// Phương thức này sử dụng thủ tục lưu trữ Proc_UpdateEmployee để cập nhật thông tin nhân viên trong cơ sở dữ liệu.
        /// Sử dụng phương thức ExecuteAsync của IDbConnection để thực hiện thủ tục lưu trữ và trả về số dòng bị ảnh hưởng.
        /// Trả về true nếu số dòng bị ảnh hưởng lớn hơn 0, ngược lại trả về false.
        /// </remarks>
        /// Created by: ldtuan (17/05/2023)
        public async Task<bool> UpdateEmployee(Guid id, EmployeeDto employeeDto)
        {
            using (IDbConnection conn = GetConnection())
            {
                conn.Open();
                var oldEmployee = await GetEmployeeById(id);
                if(oldEmployee != null)
                {
                    var parameters = new
                    {
                        p_EmployeeId = id,
                        p_EmployeeCode = employeeDto.EmployeeCode,
                        p_FullName = employeeDto.FullName,
                        p_Gender = GenderExtension.ConvertStringGender(employeeDto.Gender.ToLower()),
                        p_DateOfBirth = employeeDto.DateOfBirth,
                        p_Email = employeeDto.Email,
                        p_Mobile = employeeDto.Mobile,
                        p_DepartmentId = employeeDto.DepartmentId,
                        p_ModifiedDate = DateTime.Now,
                        p_ModifiedBy = employeeDto.ModifiedBy
                    };
                    var affectedRow = await conn.ExecuteAsync("Proc_UpdateEmployee", parameters, commandType: CommandType.StoredProcedure);
                    return affectedRow > 0;
                }
                else
                {
                    return false;
                }
            }
        }
        /// <summary>
        /// Xóa 1 nhân viên
        /// </summary>
        /// <param name="id">Id của nhân viên mà người dùng chọn để xóa.</param>
        /// <returns>Kết quả xóa nhân viên.</returns>
        /// <remarks>
        /// Phương thức này sử dụng thủ tục lưu trữ Proc_DeleteEmployeeById để xóa một nhân viên khỏi cơ sở dữ liệu.
        /// Sử dụng phương thức ExecuteAsync của IDbConnection để thực hiện thủ tục lưu trữ và trả về số dòng bị ảnh hưởng.
        /// Trả về true nếu số dòng bị ảnh hưởng lớn hơn 0, ngược lại trả về false.
        /// </remarks>
        /// Created by: ldtuan (17/05/2023)
        public async Task<bool> DeleteEmployee(Guid id)
        {
            using (IDbConnection conn = GetConnection())
            {
                conn.Open();
                var parameters = new
                {
                    p_EmployeeId = id
                };
                var affectedRow = await conn.ExecuteAsync("Proc_DeleteEmployeeById", parameters, commandType: CommandType.StoredProcedure);
                return affectedRow > 0;
            }
        }
        /// <summary>
        /// Lọc và phân trang các nhân viên
        /// </summary>
        /// <param name="pageNumber">Số trang mà người dùng đang ở.</param>
        /// <param name="pageLimit">Số lượng tối đa nhân viên mà 1 trang có thể hiển thị.</param>
        /// <param name="filterName">Tên nhân viên để lọc, có thể null.</param>
        /// <returns>Danh sách nhân viên được phân trang và lọc.</returns>
        /// <remarks>
        /// Phương thức này sử dụng thủ tục lưu trữ Proc_FilterEmployee để lọc và phân trang các nhân viên trong cơ sở dữ liệu.
        /// Nếu không có tên nhân viên thì sẽ phân trang theo danh sách tất cả các nhân viên.
        /// </remarks>
        /// Created by: ldtuan (17/05/2023)
        public async Task<IEnumerable<Employee>> FilterAndPagingEmployees(int pageNumber, int pageLimit, string filterName)
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
                return await conn.QueryAsync<Employee>("Proc_FilterEmployee", parameters, commandType: CommandType.StoredProcedure);
            }
        }
    }
}
