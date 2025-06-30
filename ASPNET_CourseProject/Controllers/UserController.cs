using System.Net.Mail;
using ASPNET_CourseProject.Filters;
using ASPNET_CourseProject.Models.Containers;
using ASPNET_CourseProject.Models.DTO;
using ASPNET_CourseProject.Models.View;
using ASPNET_CourseProject.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASPNET_CourseProject.Controllers
{
    public class UserController : Controller // TODO break into AuthorizarionController and UserController
    {
        private IUserService _userService;
        private IArtService _artService;

        public UserController(IUserService userService, IArtService artService)
        {
            _userService = userService;
            _artService = artService;
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

        [HttpGet]
        [Route("{username}/profile")]
        public IActionResult Profile(string username)
        {
            try
            {
                ProfileModel pageModel = new ProfileModel();
                pageModel.User = _userService.GetByUsername(username);
                pageModel.UserArt = _artService.GetPreviewArt(username);
                pageModel.Profile = _userService.GetProfileByUsername(username);
                return View(pageModel);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [UserSpecificFilter]
        [HttpGet]
        [Route("{username}/edit")]
        public IActionResult EditProfileGet(string username)
        {
            try
            {
                FormView<UserProfileDTO> pageModel = new FormView<UserProfileDTO> { Entity = _userService.GetProfileByUsername(username) };
                return View("/Views/User/EditProfile.cshtml", pageModel);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        //[Route("{username}/edit")]
        [Route("editProfile")]
        public IActionResult EditProfile(FormView<UserProfileDTO> pageModel)
        {
            // TODO error handling
            _userService.UpdateProfile(pageModel.Entity.Username, pageModel.Entity);
            return RedirectToAction("Profile", "User", new { pageModel.Entity.Username });
        }
    }
}
