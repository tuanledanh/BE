using Microsoft.AspNetCore.Mvc;
using MSIA.WebFresher032023.Demo.BL_Services.DTO.Empl;
using MSIA.WebFresher032023.Demo.BL_Services.Service.Empl;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MSIA.WebFresher032023.Demo.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeService _employeeService;
        public EmployeeController(IConfiguration configuration, IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }
        [HttpGet]
        public async Task<IActionResult> GetListEmployees([FromQuery] int pageNumber, [FromQuery] int pageLimit, [FromQuery] string? filterName)
        {
            var employees = await _employeeService.GetListAsync(pageNumber, pageLimit, filterName);
            if (employees.Any())
            {
                return Ok(employees);
            }
            else
            {
                throw new Exception("Không thể tìm thấy danh sách nhân viên");
            }
        }
        [HttpGet("{id}")]
        public async Task<EmployeeDto?> GetAsync(Guid id)
        {
            var employeeDto = await _employeeService.GetAsync(id);
            return employeeDto;
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] EmployeeCreateDto employeeCreateDto)
        {
            var result = await _employeeService.PostAsync(employeeCreateDto);
            if (result)
            {
                return Ok("Insert successfully");
            }
            else
            {
                throw new Exception("Không thể thêm mới nhân viên này");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] EmployeeUpdateDto employeeUpdateDto)
        {
            var result = await _employeeService.PutAsync(id, employeeUpdateDto);
            if (result)
            {
                return Ok("Update successfully");
            }
            else
            {
                throw new Exception("Không thể cập nhật nhân viên này");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEmployee(Guid id)
        {
            var result = await _employeeService.DeleteAsync(id);
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
