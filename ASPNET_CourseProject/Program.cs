using ASPNET_CourseProject.Services;
using ASPNET_CourseProject.Services.Implementations;

namespace ASPNET_CourseProject
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<IArtService, ArtService>();

            var app = builder.Build();

            //app.MapGet("/", () => "Hello World!");
            //app.MapControllers();
            app.MapControllerRoute("default", "{controller=Home}/{action=Index}");

            app.UseStaticFiles();

            app.Run();
        }
    }
}
