using ASPNET_CourseProject.Data;
using ASPNET_CourseProject.Services;
using ASPNET_CourseProject.Services.Implementations;
using Microsoft.EntityFrameworkCore;

namespace ASPNET_CourseProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddSession();

            builder.Services.AddDbContext<AppDbContext>
                (
                    options =>
                    {
                        string? connStr = builder.Configuration.GetConnectionString("Default") 
                        ?? throw new MissingFieldException("Failed to get default connection string! Please check your appsettings.");
                        options.UseSqlServer(connStr, o => o.UseCompatibilityLevel(170));
                        //options.UseValidationCheckConstraints();
                    }
                );

            builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            builder.Services.AddScoped<IArtService, ArtService>();
            builder.Services.AddScoped<IUserService, UserService>();

            var app = builder.Build();

            app.UseSession();

            //app.MapGet("/", () => "Hello World!");
            //app.MapControllers();
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}");

            app.UseStaticFiles();

            app.Run();
        }
    }
}
