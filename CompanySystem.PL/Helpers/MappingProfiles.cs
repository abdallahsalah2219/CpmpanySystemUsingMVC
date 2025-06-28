using AutoMapper;
using CompanySystem.PL.ViewModels;
using DAL.Models;

namespace CompanySystem.PL.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
        }
    }
}
