using AutoMapper;
using MSIA.WebFresher032023.Demo.BL_Services.DTO.Depart;
using MSIA.WebFresher032023.Demo.DL_Repositories.Entity;

namespace MSIA.WebFresher032023.Demo.BL_Services.AutoMapper
{
    public class DepartmentProfile : Profile
    {
        public DepartmentProfile()
        {
            CreateMap<Department, DepartmentDto>();
            CreateMap<DepartmentCreateDto, Department>();
            CreateMap<DepartmentUpdateDto, Department>();
        }
    }
}
