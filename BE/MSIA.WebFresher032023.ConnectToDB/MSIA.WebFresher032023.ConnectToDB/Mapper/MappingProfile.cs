using AutoMapper;
using MSIA.WebFresher032023.ConnectToDB.EnumExtension;
using MSIA.WebFresher032023.ConnectToDB.Mapper.DTO;
using MSIA.WebFresher032023.ConnectToDB.Models;

namespace MSIA.WebFresher032023.ConnectToDB.Mapper
{
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Thực hiện ánh xạ dữ liệu
        /// </summary>
        public MappingProfile()
        {
            CreateMap<Employee, EmployeeDto>()
                .ForMember(dest => dest.Gender, opt => opt.MapFrom(src => GenderExtension.ConvertEnumGender(src.Gender)));
        }
    }
}
