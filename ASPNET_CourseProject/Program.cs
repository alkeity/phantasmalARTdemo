using ASPNET_CourseProject.Data;
using ASPNET_CourseProject.Filters;
using ASPNET_CourseProject.Services;
using ASPNET_CourseProject.Services.Implementations;
using Microsoft.EntityFrameworkCore;
using Minio;

namespace ASPNET_CourseProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();
            builder.Services.AddSession();

            // database
            builder.Services.AddDbContext<AppDbContext>
                (
                    options =>
                    {
                        string connStr = builder.Configuration.GetConnectionString("Default") 
                        ?? throw new MissingFieldException(
                            "Failed to get default connection string! Please check your appsettings."
                            );
                        options.UseNpgsql(connStr);
                        //options.UseValidationCheckConstraints();
                    }
                );

            // object storage
            // TODO exceptions
            builder.Services.AddMinio(
                configureClient => configureClient
                .WithEndpoint(
                    builder.Configuration.GetSection("ObjectStorage").GetValue<string>("Endpoint")
                    )
                .WithCredentials(
                    builder.Configuration.GetSection("ObjectStorage").GetValue<string>("Username"),
                    builder.Configuration.GetSection("ObjectStorage").GetValue<string>("Password")
                    )
                .WithSSL(false)
                .Build()
                );

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<IArtService, ArtService>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IStorageService, StorageService>();
            builder.Services.AddScoped<UserAuthFilter>();

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
