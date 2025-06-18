using Microsoft.AspNetCore.Mvc;

namespace ASPNET_CourseProject.Controllers
{
    public class ArtController : Controller
    {
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
    }
}
