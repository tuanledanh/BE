using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using MSIA.WebFresher032023.ConnectToDB.Enum;
using MSIA.WebFresher032023.ConnectToDB.EnumExtension;
using MSIA.WebFresher032023.ConnectToDB.Mapper.DTO;
using MSIA.WebFresher032023.ConnectToDB.Models;
using MSIA.WebFresher032023.ConnectToDB.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MSIA.WebFresher032023.ConnectToDB.Controllers
{
    /// <summary>
    /// Controller quản lý các nhân viên.
    /// </summary>
    /// Created by: ldtuan (17/05/2023)
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeeController(EmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Lấy danh sách các nhân viên.
        /// </summary>
        /// <returns>Danh sách các nhân viên.</returns>
        /// <remarks>
        /// </remarks>
        /// Created by: ldtuan (17/05/2023)
        [HttpGet]
        public async Task<IActionResult> GetListEmployees()
        {
            var employees = await _employeeRepository.GetListEmployees();
            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            if (employeeDtos.Any())
            {
                return Ok(employeeDtos);
            }
            else
            {
                throw new Exception("Không thể tìm thấy danh sách nhân viên");
            }
        }

        /// <summary>
        /// Lấy thông tin nhân viên theo id.
        /// </summary>
        /// <param name="id">Id của nhân viên cần lấy thông tin.</param>
        /// <returns>Thông tin nhân viên.</returns>
        /// <remarks>
        /// </remarks>
        /// Created by: ldtuan (17/05/2023)
        [HttpGet("{id}")]
        public async Task<IActionResult> GetEmployeeById(Guid id)
        {
            var employee = await _employeeRepository.GetEmployeeById(id);

            if (employee == null)
            {
                throw new Exception("Không thể tìm thấy nhân viên này");
            }

            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return Ok(employeeDto);
        }

        /// <summary>
        /// Lọc và phân trang các nhân viên.
        /// </summary>
        /// <param name="pageNumber">Số trang cần lấy.</param>
        /// <param name="pageLimit">Số lượng nhân viên tối đa trên mỗi trang.</param>
        /// <param name="filterName">Tên nhân viên cần lọc, có thể null.</param>
        /// <returns>Danh sách các nhân viên đã lọc và phân trang.</returns>
        /// <remarks>
        /// Endpoint này lọc và phân trang các nhân viên, nếu không có điều kiện lọc,
        /// thì hàm sẽ thực hiện phân trang theo danh sách tất cả các nhân viên.
        /// </remarks>
        /// Created by: ldtuan (17/05/2023)
        [HttpGet("search")]
        public async Task<IActionResult> FilterAndPagingEmployees([FromQuery] int pageNumber, [FromQuery] int pageLimit, [FromQuery] string? filterName)
        {
            var employees = await _employeeRepository.FilterAndPagingEmployees(pageNumber, pageLimit, filterName);
            var employeeDtos = _mapper.Map<IEnumerable<EmployeeDto>>(employees);
            if (employeeDtos.Any())
            {
                return Ok(employeeDtos);
            }
            else
            {
                throw new Exception("Không thể tìm thấy nhân viên này");
            }
        }

        /// <summary>
        /// Thêm mới 1 nhân viên.
        /// </summary>
        /// <param name="Employee">Thông tin nhân viên mới do người dùng nhập vào.</param>
        /// <returns>Kết quả thêm nhân viên.</returns>
        /// <remarks>
        /// Endpoint này thêm một nhân viên mới và trả về kết quả thành công hoặc thất bại.
        /// </remarks>
        /// Created by: ldtuan (17/05/2023)
        [HttpPost]
        public async Task<IActionResult> InsertEmployee([FromBody] EmployeeDto employeeDto)
        {
            var result = await _employeeRepository.InsertEmployee(employeeDto);
            if (result)
            {
                return Ok("Insert successfully");
            }
            else
            {
                throw new Exception("Không thể thêm mới nhân viên này");
            }
        }

        /// <summary>
        /// Cập nhật thông tin 1 nhân viên.
        /// </summary>
        /// <param name="id">Id của nhân viên cần cập nhật.</param>
        /// <param name="Employee">Thông tin mới của nhân viên để cập nhật.</param>
        /// <returns>Kết quả cập nhật nhân viên.</returns>
        /// <remarks>
        /// Endpoint này cập nhật thông tin của một nhân viên dựa trên ID và thông tin mới của nhân viên và
        /// trả về kết quả thành công hoặc thất bại.
        /// </remarks>
        /// Created by: ldtuan (17/05/2023)
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] EmployeeDto employeeDto)
        {
            var result = await _employeeRepository.UpdateEmployee(id, employeeDto);
            if (result)
            {
                return Ok("Update successfully");
            }
            else
            {
                throw new Exception("Không thể cập nhật nhân viên này");
            }
        }

        /// <summary>
        /// Xóa 1 nhân viên.
        /// </summary>
        /// <param name="id">Id của nhân viên cần xóa.</param>
        /// <returns>Kết quả xóa nhân viên.</returns>
        /// <remarks>
        /// Endpoint này xóa một nhân viên dựa trên ID và trả về kết quả thành công hoặc thất bại.
        /// </remarks>
        /// Created by: ldtuan (17/05/2023)
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var result = await _employeeRepository.DeleteEmployee(id);
            if (result)
            {
                return Ok("Delete successfully");
            }
            else
            {
                throw new Exception("Không thể xóa nhân viên này");
            }
        }
    }
}
