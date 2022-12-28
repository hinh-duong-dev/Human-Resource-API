using AutoMapper;
using Entities.DTOs;
using Entities.Models;

namespace HumanResourceAPI
{
    public class MappingProfile : Profile
    {
        public MappingProfile() 
        {
            CreateMap<Company, CompanyDto>()
                 .ForMember(c => c.FullAddress, opt => opt.MapFrom(x => $"{x.Address}, {x.Country}"));

            CreateMap<Company, CompanyManipulationDto>().ReverseMap();

            CreateMap<Employee, EmployeeDto>();
            CreateMap<Employee, EmployeeManipulationDto>().ReverseMap();
        }
    }
}
