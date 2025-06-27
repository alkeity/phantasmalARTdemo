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

            HttpContext.Session.SetString("UserName", pageModel.User.Username);

            return RedirectToAction("Index", "Home"); // TODO redirect to profile
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

            HttpContext.Session.SetString("UserName", pageModel.User.Username);

            return RedirectToAction("Index", "Home"); // TODO redirect to profile
        }

        [Route("logout")]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        [Route("{username}/profile")]
        public IActionResult Profile(string username, ProfileModel pageModel)
        {
            Console.WriteLine($"Profile for: {username}");
            pageModel.User = _userService.GetByUsername(username);
            pageModel.UserArt = _artService.GetPreviewArt(username);
            pageModel.Profile = _userService.GetProfileByUsername(username);
            return View(pageModel);
        }

        [HttpGet]
        [Route("{username}/edit")]
        public IActionResult EditProfileGet(string username)
        {
            UserProfileDTO profileModel = _userService.GetProfileByUsername(username);
            return View("/Views/User/EditProfile.cshtml", profileModel);
        }

        [HttpPost]
        [Route("{username}/edit")]
        public IActionResult EditProfile(string username, UserProfileDTO profileModel)
        {
            return View(profileModel);
        }
    }
}
