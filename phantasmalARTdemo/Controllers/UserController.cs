using System.Net.Mail;
using PhantasmalARTdemo.Filters;
using PhantasmalARTdemo.Models.Containers;
using PhantasmalARTdemo.Models.DTO;
using PhantasmalARTdemo.Models.View;
using PhantasmalARTdemo.Services;
using Microsoft.AspNetCore.Mvc;

namespace PhantasmalARTdemo.Controllers
{
    public class UserController : Controller // TODO break into AuthorizarionController and UserController
    {
        private IUserService _userService;
        private IArtService _artService;
        private IHttpContextAccessor _contextAccessor;

        public UserController(IUserService userService, IArtService artService, IHttpContextAccessor contextAccessor)
        {
            _userService = userService;
            _artService = artService;
            _contextAccessor = contextAccessor;
        }

        [HttpGet]
        [Route("{username}/profile")]
        public IActionResult Profile(string username)
        {
            try
            {
                ProfileModel pageModel = new ProfileModel();
                pageModel.User = _userService.GetByUsername(username);
                if (pageModel.User.IsDeleted)
                {
                    return NotFound();
                }
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
            // TODO add checks
            try
            {
                BaseFormView<UserProfileDTO> pageModel = new BaseFormView<UserProfileDTO> { Entity = _userService.GetProfileByUsername(username) };
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
        public IActionResult EditProfile(BaseFormView<UserProfileDTO> pageModel)
        {
            // TODO error handling and checks
            try
            {
                string? username = _contextAccessor.HttpContext.Session.GetString("UserName");
                _userService.UpdateProfile(username, pageModel.Entity);
                return RedirectToAction("Profile", "User", new { username });
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpGet]
        [Route("delete_account")]
        public IActionResult Delete()
        {
            // TODO error handling and checks
            string? username = _contextAccessor.HttpContext.Session.GetString("UserName");
            Console.WriteLine($"CALLED DELETE: User {username} requests deletion");
            try
            {
                // get user and swap isDeleted
                UserDTO user = _userService.GetByUsername(username);
                Console.WriteLine($"User {username} {(user != null ? "" : "does not")} exists");
                if (user != null)
                {
                    user.IsDeleted = true;
                    _userService.Update(user);
                }
                return RedirectToAction("Logout", "Auth");
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }
    }
}
