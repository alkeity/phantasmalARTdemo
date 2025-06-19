using Microsoft.AspNetCore.Mvc;

namespace ASPNET_CourseProject.Controllers
{
    public class ArtController : Controller
    {
        [HttpGet]
        [Route("{username}/gallery/{artTitle}-{artID:Guid}")]
        public IActionResult ArtDisplay(string username, string artTitle, Guid artID)
        {
            return View();
        }
    }
}
