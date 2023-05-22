using AutoMapper;
using MSIA.WebFresher032023.Demo.BL_Services.DTO.Empl;
using MSIA.WebFresher032023.Demo.DL_Repositories.Entity;
using MSIA.WebFresher032023.Demo.DL_Repositories.Repositories.Empl;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MSIA.WebFresher032023.Demo.BL_Services.Service.Empl
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        private readonly IMapper _mapper;
        public EmployeeService(IEmployeeRepository employeeRepository, IMapper mapper)
        {
            _employeeRepository = employeeRepository;
            _mapper = mapper;
        }
        public async Task<EmployeeDto> GetAsync(Guid employeeId)
        {
            var employee = await _employeeRepository.GetAsync(employeeId);
            if (employee == null)
            {
                return null;
            }
            var employeeDto = _mapper.Map<EmployeeDto>(employee);
            return employeeDto;
        }

        public async Task<List<EmployeeDto>> GetListAsync(int pageNumber, int pageLimit, string filterName)
        {
            var employees = await _employeeRepository.GetListAsync(pageNumber, pageLimit, filterName);
            List<EmployeeDto> employeeDtos = new List<EmployeeDto>();
            foreach (var employee in employees)
            {
                employeeDtos.Add(_mapper.Map<EmployeeDto>(employee));
            }

            if (employeeDtos.Any())
            {
                return employeeDtos;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> PostAsync(EmployeeCreateDto employeeCreateDto)
        {
            var employee = _mapper.Map<Employee>(employeeCreateDto);
            bool result = await _employeeRepository.PostAsync(employee);
            if (!result)
            {
                throw new Exception("Can not add employee");
            }
            else
            {
                return result;
            }
        }

        public async Task<bool> PutAsync(Guid employeeId, EmployeeUpdateDto employeeUpdateDto)
        {
            var employee = _mapper.Map<Employee>(employeeUpdateDto);
            var result = await _employeeRepository.PutAsync(employeeId, employee);
            if (!result)
            {
                throw new Exception("Can not update employee");
            }
            else
            {
                return result;
            }
        }

        public async Task<bool> DeleteAsync(Guid employeeId)
        {
            var result = await _employeeRepository.DeleteAsync(employeeId);
            if (!result)
            {
                throw new Exception("Can not delete employee");
            }
            else
            {
                return result;
            }
        }
    }
}
