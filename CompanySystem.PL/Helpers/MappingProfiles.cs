using AutoMapper;
using CompanySystem.PL.ViewModels;
using DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace CompanySystem.PL.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<EmployeeViewModel, Employee>().ReverseMap();
            CreateMap<ApplicationUser, UserViewModel>().ReverseMap();
            CreateMap<RoleViewModel, IdentityRole>()
                .ForMember(d=>d.Name , o=>o.MapFrom(s=>s.RoleName)).ReverseMap();
        }
    }
}
