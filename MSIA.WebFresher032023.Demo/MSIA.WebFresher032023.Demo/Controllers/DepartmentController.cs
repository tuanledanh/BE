using Microsoft.AspNetCore.Mvc;
using MSIA.WebFresher032023.Demo.BL_Services.DTO.Depart;
using MSIA.WebFresher032023.Demo.BL_Services.Service.Depart;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace MSIA.WebFresher032023.Demo.Controllers
{
    [Route("api/v1/[controller]s")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IDepartmentService _departmentService;
        public DepartmentController(IConfiguration configuration, IDepartmentService departmentService)
        {
            _departmentService = departmentService;
        }
        [HttpGet]
        public async Task<IActionResult> GetListDepartments([FromQuery] int pageNumber, [FromQuery] int pageLimit, [FromQuery] string? filterName)
        {
            var departments = await _departmentService.GetListAsync(pageNumber, pageLimit, filterName);
            if (departments.Any())
            {
                return Ok(departments);
            }
            else
            {
                throw new Exception("Không thể tìm thấy danh sách phòng ban");
            }
        }
        [HttpGet("{id}")]
        public async Task<DepartmentDto?> GetAsync(Guid id)
        {
            var departmentDto = await _departmentService.GetAsync(id);
            return departmentDto;
        }
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] DepartmentCreateDto departmentCreateDto)
        {
            var result = await _departmentService.PostAsync(departmentCreateDto);
            if (result)
            {
                return Ok("Insert successfully");
            }
            else
            {
                throw new Exception("Không thể thêm mới phòng ban này");
            }
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAsync(Guid id, [FromBody] DepartmentUpdateDto departmentUpdateDto)
        {
            var result = await _departmentService.PutAsync(id, departmentUpdateDto);
            if (result)
            {
                return Ok("Update successfully");
            }
            else
            {
                throw new Exception("Không thể cập nhật phòng ban này");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(Guid id)
        {
            var result = await _departmentService.DeleteAsync(id);
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
