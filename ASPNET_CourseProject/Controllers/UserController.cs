using ASPNET_CourseProject.Models.DTO;
using ASPNET_CourseProject.Models.View;
using ASPNET_CourseProject.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

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
        [Route("login")]
        public IActionResult LoginGet(AuthViewModel pageModel)
        {
            return View("/Views/User/Login.cshtml", pageModel);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(AuthViewModel pageModel)
        {
            List<string>? errors;
            pageModel.User = _userService.ConfirmUser(pageModel.User, out errors);
            if (errors != null)
            {
                pageModel.Errors = errors;
                return View(pageModel);
            }

            HttpContext.Session.SetString("UserName", pageModel.User.Username);

            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("register")]
        public IActionResult RegisterGet(AuthViewModel pageModel)
        {
            return View("/Views/User/Register.cshtml", pageModel);

        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(AuthViewModel pageModel)
        {
            pageModel.Errors = _userService.Add(pageModel.User);
            if (pageModel.Errors != null)
            {
                return View(pageModel);
            }

            HttpContext.Session.SetString("UserName", pageModel.User.Username);

            return RedirectToAction("Index", "Home");
        }

        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("{username}")]
        public IActionResult Profile(string username, ProfileViewModel pageModel)
        {
            pageModel.User = _userService.GetByUsername(username);
            return View(pageModel);
        }
    }
}
