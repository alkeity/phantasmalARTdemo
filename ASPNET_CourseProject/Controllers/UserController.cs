using ASPNET_CourseProject.Models.DTO;
using ASPNET_CourseProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_CourseProject.Controllers
{
    public class UserController : Controller
    {
        private IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("auth")]
        public IActionResult Auth()
        {
            return View();
        }

        [HttpPost]
        [Route("auth/login")]
        public IActionResult Login(UserDTO userInfo)
        {
            switch (_userService.ConfirmUser(userInfo)) // TODO maybe better use enums?
            {
                case 0:
                    // login user, redirect to home/profile
                    break;
                case 1:
                    // username wrong, stay on page
                    break;
                case 2:
                    // password wrong, stay on page
                    break;
            }
            return RedirectToAction("Index", "Home"); // stub
        }

        [HttpPost]
        [Route("auth/register")]
        public IActionResult Register(UserDTO userInfo)
        {
            if (_userService.Add(userInfo))
            {
                // TODO login and redirect home (or to profile?)
                return RedirectToAction("Index", "Home");
            }
            // TODO return to page with errors
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("{username}")]
        public IActionResult Profile(string username)
        {
            return View();
        }
    }
}
