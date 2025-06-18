using ASPNET_CourseProject.Models.Containers;
using ASPNET_CourseProject.Models.DTO;
using ASPNET_CourseProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_CourseProject.Controllers
{
    public class HomeController : Controller
    {
        private IArtService _artService;

        public HomeController(IArtService artService)
        {
            _artService = artService;
        }
        public IActionResult Index(int page = 0)
        {
            page = Math.Clamp(page, 0, int.MaxValue);
            Page<ArtDTO> model = _artService.GetArt(page);
            return View(model);
        }
    }
}
