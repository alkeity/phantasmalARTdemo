using Microsoft.AspNetCore.Mvc;

namespace ASPNET_CourseProject.Controllers
{
    public class UserController : Controller
    {
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult LoginPost()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegisterPost()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Profile()
        {
            return View();
        }
    }
}
