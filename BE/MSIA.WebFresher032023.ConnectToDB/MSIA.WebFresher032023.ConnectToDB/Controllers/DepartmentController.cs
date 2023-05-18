using Microsoft.AspNetCore.Mvc;
using MSIA.WebFresher032023.ConnectToDB.Models;
using MSIA.WebFresher032023.ConnectToDB.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MSIA.WebFresher032023.ConnectToDB.Controllers
{
    /// <summary>
    /// Controller quản lý các phòng ban.
    /// </summary>
    /// Created by: ldtuan (17/05/2023)
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly DepartmentRepository _departmentRepository;
        public DepartmentController(DepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        /// <summary>
        /// Lấy danh sách các phòng ban.
        /// </summary>
        /// <returns>Danh sách các phòng ban.</returns>
        /// <remarks>
        /// </remarks>
        /// Created by: ldtuan (17/05/2023)
        [HttpGet]
        public async Task<IActionResult> GetListDepartments()
        {
            var departments = await _departmentRepository.GetListDepartments();
            if (departments.Any())
            {
                return Ok(departments);
            }
            else
            {
                throw new Exception("Không thể tìm thấy danh sách phòng ban");
            }
        }

        /// <summary>
        /// Lấy thông tin phòng ban theo id.
        /// </summary>
        /// <param name="id">Id của phòng ban cần lấy thông tin.</param>
        /// <returns>Thông tin phòng ban.</returns>
        /// <remarks>
        /// </remarks>
        /// Created by: ldtuan (17/05/2023)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetDepartMentById(Guid id)
        {
            var department = await _departmentRepository.GetDepartmentById(id);
            if (department == null)
            {
                throw new Exception("Không thể tìm thấy phòng ban này");
            }
            return Ok(department);
        }

        /// <summary>
        /// Lọc và phân trang các phòng ban.
        /// </summary>
        /// <param name="pageNumber">Số trang cần lấy.</param>
        /// <param name="pageLimit">Số lượng phòng ban tối đa trên mỗi trang.</param>
        /// <param name="filterName">Tên phòng ban cần lọc, có thể null.</param>
        /// <returns>Danh sách các phòng ban đã lọc và phân trang.</returns>
        /// <remarks>
        /// Endpoint này lọc và phân trang các phòng ban, nếu không có điều kiện lọc,
        /// thì hàm sẽ thực hiện phân trang theo danh sách tất cả các phòng ban.
        /// </remarks>
        /// Created by: ldtuan (17/05/2023)
        [HttpGet("search")]
        public async Task<IActionResult> FilterAndPagingDepartments([FromQuery]int pageNumber, [FromQuery] int pageLimit, [FromQuery] string? filterName)
        {
            var departments = await _departmentRepository.FilterAndPagingDepartments(pageNumber, pageLimit, filterName);
            if (departments.Any())
            {
                return Ok(departments);
            }
            else
            {
                throw new Exception("Không thể tìm thấy phòng ban này");
            }
        }

        /// <summary>
        /// Thêm mới 1 phòng ban.
        /// </summary>
        /// <param name="department">Thông tin phòng ban mới do người dùng nhập vào.</param>
        /// <returns>Kết quả thêm phòng ban.</returns>
        /// <remarks>
        /// Endpoint này thêm một phòng ban mới và trả về kết quả thành công hoặc thất bại.
        /// </remarks>
        /// Created by: ldtuan (17/05/2023)
        [HttpPost]
        public async Task<IActionResult> InsertDepartment([FromBody] Department department)
        {
            var result = await _departmentRepository.InsertDepartment(department);
            if (result)
            {
                return Ok("Insert successfully");
            }
            else
            {
                throw new Exception("Không thể thêm mới phòng ban này");
            }
        }

        /// <summary>
        /// Cập nhật thông tin 1 phòng ban.
        /// </summary>
        /// <param name="id">Id của phòng ban cần cập nhật.</param>
        /// <param name="department">Thông tin mới của phòng ban để cập nhật.</param>
        /// <returns>Kết quả cập nhật phòng ban.</returns>
        /// <remarks>
        /// Endpoint này cập nhật thông tin của một phòng ban dựa trên ID và thông tin mới của phòng ban và
        /// trả về kết quả thành công hoặc thất bại.
        /// </remarks>
        /// Created by: ldtuan (17/05/2023)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateDepartment(Guid id, [FromBody] Department department)
        {
            var result = await _departmentRepository.UpdateDepartment(id, department);
            if (result)
            {
                return Ok("Update successfully");
            }
            else
            {
                throw new Exception("Không thể cập nhật phòng ban này");
            }
        }

        /// <summary>
        /// Xóa 1 phòng ban.
        /// </summary>
        /// <param name="id">Id của phòng ban cần xóa.</param>
        /// <returns>Kết quả xóa phòng ban.</returns>
        /// <remarks>
        /// Endpoint này xóa một phòng ban dựa trên ID và trả về kết quả thành công hoặc thất bại.
        /// </remarks>
        /// Created by: ldtuan (17/05/2023)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDepartment(Guid id)
        {
            var result = await _departmentRepository.DeleteDepartment(id);
            if (result)
            {
                return Ok("Delete successfully");
            }
            else
            {
                throw new Exception("Không thể xóa phòng ban này");
            }
        }
    }
}
