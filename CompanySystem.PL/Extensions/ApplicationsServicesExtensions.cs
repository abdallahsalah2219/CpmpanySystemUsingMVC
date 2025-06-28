using BLL.Interfaces;
using BLL.Repositories;
using CompanySystem.PL.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace CompanySystem.PL.Extensions
{
    public static class ApplicationsServicesExtensions
    {
        public static IServiceCollection AddApplicationsServices(this IServiceCollection services)
        {
            services.AddScoped<IDepartmentRepository, DepartmentRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();
            // services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));


            services.AddAutoMapper(M=>M.AddProfile(new MappingProfiles()));


            return services;
        }
    }
}
