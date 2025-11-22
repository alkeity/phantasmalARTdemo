using System.Net.Mail;
using ASPNET_CourseProject.Filters;
using ASPNET_CourseProject.Models.View;
using ASPNET_CourseProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_CourseProject.Controllers
{
    public class AuthController : Controller
    {
        private IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("login")]
        public IActionResult LoginGet(AuthModel pageModel)
        {
            return View("/Views/User/Login.cshtml", pageModel);
        }

        [HttpPost]
        [Route("login")]
        public IActionResult Login(AuthModel pageModel)
        {
            List<string>? errors;
            pageModel.User = _userService.ConfirmUser(pageModel.User, out errors);
            if (errors != null)
            {
                pageModel.Errors = errors;
                return View(pageModel);
            }

            try
            {
                MailAddress email = new MailAddress(pageModel.User.Email);
            }
            catch (FormatException)
            {
                pageModel.Errors = new List<string>();
                pageModel.Errors.Add("Invalid email address");
                return View(pageModel);
            }

            HttpContext.Session.SetString("UserName", pageModel.User.Username);

            return RedirectToAction("Profile", "User", new { username = pageModel.User.Username });
        }

        [HttpGet]
        [Route("register")]
        public IActionResult RegisterGet(AuthModel pageModel)
        {
            return View("/Views/User/Register.cshtml", pageModel);

        }

        [HttpPost]
        [Route("register")]
        public IActionResult Register(AuthModel pageModel)
        {
            pageModel.Errors = _userService.Add(pageModel.User);
            if (pageModel.Errors != null)
            {
                return View(pageModel);
            }

            try
            {
                MailAddress email = new MailAddress(pageModel.User.Email);
            }
            catch (FormatException)
            {
                pageModel.Errors = new List<string>();
                pageModel.Errors.Add("Invalid email address");
                return View(pageModel);
            }

            HttpContext.Session.SetString("UserName", pageModel.User.Username);

            return RedirectToAction("Profile", "User", new { username = pageModel.User.Username });
        }

        [UserAuthFilter]
        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
