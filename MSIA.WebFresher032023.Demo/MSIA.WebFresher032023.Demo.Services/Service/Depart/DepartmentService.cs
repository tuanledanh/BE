using AutoMapper;
using MSIA.WebFresher032023.Demo.BL_Services.DTO;
using MSIA.WebFresher032023.Demo.BL_Services.DTO.Depart;
using MSIA.WebFresher032023.Demo.DL_Repositories.Entity;
using MSIA.WebFresher032023.Demo.DL_Repositories.Repositories;
using MSIA.WebFresher032023.Demo.DL_Repositories.Repositories.Depart;

namespace MSIA.WebFresher032023.Demo.BL_Services.Service.Depart
{
    public class DepartmentService : BaseService<Department, DepartmentDto, DepartmentUpdateDto, DepartmentCreateDto>, IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly IMapper _mapper;
        public DepartmentService(IDepartmentRepository departmentRepository, IMapper mapper) : base(departmentRepository, mapper)
        {
            _departmentRepository = departmentRepository;
            _mapper = mapper;
        }
        public async Task<DepartmentDto> GetAsync(Guid departmentId)
        {
            var department = await _departmentRepository.GetAsync(departmentId);
            if (department == null)
            {
                return null;
            }
            var departmentDto = _mapper.Map<DepartmentDto>(department);
            return departmentDto;
        }

        public async Task<List<DepartmentDto>> GetListAsync(int pageNumber, int pageLimit, string filterName)
        {
            var departments = await _departmentRepository.GetListAsync(pageNumber, pageLimit, filterName);
            List<DepartmentDto> departmentDtos = new List<DepartmentDto>();
            foreach (var department in departments)
            {
                departmentDtos.Add(_mapper.Map<DepartmentDto>(department));
            }

            if (departmentDtos.Any())
            {
                return departmentDtos;
            }
            else
            {
                return null;
            }
        }

        public async Task<bool> PostAsync(DepartmentCreateDto departmentCreateDto)
        {
            var department = _mapper.Map<Department>(departmentCreateDto);
            bool result = await _departmentRepository.PostAsync(department);
            if (!result)
            {
                throw new Exception("Can not add department");
            }
            else
            {
                return result;
            }
        }

        public async Task<bool> PutAsync(Guid departmentId, DepartmentUpdateDto departmentUpdateDto)
        {
            var department = _mapper.Map<Department>(departmentUpdateDto);
            var result = await _departmentRepository.PutAsync(departmentId, department);
            if (!result)
            {
                throw new Exception("Can not update department");
            }
            else
            {
                return result;
            }
        }
        public async Task<bool> DeleteAsync(Guid departmentId)
        {
            var result = await _departmentRepository.DeleteAsync(departmentId);
            if (!result)
            {
                throw new Exception("Can not delete department");
            }
            else
            {
                return result;
            }
        }
    }
}
