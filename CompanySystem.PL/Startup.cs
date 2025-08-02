using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interfaces;
using BLL.Repositories;
using CompanySystem.PL.Extensions;
using DAL.Data;
using DAL.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace CompanySystem.PL
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            services.AddDbContext<AppDbContext>(options => 
            {
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });

            services.AddApplicationsServices(); // Extension Method

            //services.AddScoped<UserManager<ApplicationUser>>();
            //services.AddScoped<SignInManager<ApplicationUser>>();
            //services.AddScoped<RoleManager<IdentityRole>>();

            services.AddIdentity<ApplicationUser,IdentityRole>(config=>
            {
                // Password
                config.Password.RequiredUniqueChars = 2;
                config.Password.RequireDigit = true;
                config.Password.RequireNonAlphanumeric = true;
                config.Password.RequiredLength = 5;
                config.Password.RequireUppercase = true;
                config.Password.RequireLowercase = true;

                // User
                config.User.RequireUniqueEmail = true;

                //LockOut
                config.Lockout.MaxFailedAccessAttempts = 5;
                config.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
                config.Lockout.AllowedForNewUsers = true;

            }).AddEntityFrameworkStores<AppDbContext>();

            services.ConfigureApplicationCookie(config => 
            {
                config.LoginPath = "/Account/SignIn";
            });

            //services.AddAuthentication("Cookies")
            //    .AddCookie("Hamada", config =>
            //    {
            //        config.LoginPath = "/Account/SignIn";
            //        config.AccessDeniedPath = "/Home/Error";
            //    });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
