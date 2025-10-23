using Campany.Joe.PL.Mapping;
using Company.BLL.Interfaces;
using Company.BLL;
using Company.BLL.Repositories;
using Company.DAL.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Company.DAL.Models;
using Microsoft.AspNetCore.Identity;

namespace Campany.Joe.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //builder.Services.AddScoped<IDepartmentRepository,DepartmentRepository>(); //Allow DI For DepartmentRepository
            //builder.Services.AddScoped<IEmployeeRepository,EmployeeRepository>(); //Allow DI For DepartmentRepository
            builder.Services.AddScoped<IUnitOfWork,UnitOfWork>();  //Allow DI For UnitOfWork

            builder.Services.AddDbContext<CampanyDbContext>(option =>
            {
                option.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            }
            );// Allow DI For CampanyDbContext

            builder.Services.AddAutoMapper(E => E.AddProfile(new EmployeeProfile()));
           
            builder.Services.AddIdentity<AppUser,IdentityRole>()//<AppUser,IdentityRole> because must use user + role 
                .AddEntityFrameworkStores<CampanyDbContext>() //use this line because constructor of ussermanager class inject object from Istorerole 
                .AddDefaultTokenProviders(); //AddDefaultTokenProviders:- this services used to generate taken in reset password
            builder.Services.ConfigureApplicationCookie(config =>
            {
                config.LoginPath = "/Account/SignIn";    //use this fuction to change login path from default ("/Account/LogIn") To signin page ("/Account/SignIn")
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication(); //Implement middleware of Authentication
            app.UseAuthorization(); //Implement middleware of Authorization
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
