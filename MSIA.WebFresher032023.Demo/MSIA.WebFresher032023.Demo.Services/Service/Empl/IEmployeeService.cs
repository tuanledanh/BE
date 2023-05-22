using MSIA.WebFresher032023.Demo.BL_Services.DTO.Empl;
using MSIA.WebFresher032023.Demo.DL_Repositories.Entity;

namespace MSIA.WebFresher032023.Demo.BL_Services.Service.Empl
{
    public interface IEmployeeService : IBaseService<Employee, EmployeeDto, EmployeeUpdateDto, EmployeeCreateDto>
    {
    }
}
