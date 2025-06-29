using ASPNET_CourseProject.Data.Models;
using ASPNET_CourseProject.Filters;
using ASPNET_CourseProject.Models.Containers;
using ASPNET_CourseProject.Models.DTO;
using ASPNET_CourseProject.Models.View;
using ASPNET_CourseProject.Services;
using ASPNET_CourseProject.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ASPNET_CourseProject.Controllers
{
    public class ArtController : Controller
    {
        IArtService _artService;
        IUserService _userService;
        readonly string _tmpFolder;

        public ArtController(IArtService artService, IUserService userService, IConfiguration config)
        {
            _artService = artService;
            _userService = userService;
            _tmpFolder = config.GetValue<string>("TempImageStorage");
        }

        [HttpGet]
        [Route("{username}/gallery")]
        public IActionResult UserGallery(string username, int page = 0)
        {
            UserGalleryModel pageModel = new UserGalleryModel();
            pageModel.User = _userService.GetByUsername(username);

            page = Math.Clamp(page, 0, int.MaxValue);
            pageModel.Art = _artService.GetArt(page, username);
            return View(pageModel);
        }

        [HttpGet]
        [Route("{username}/gallery/{artID:Guid}")]
        public IActionResult ArtDisplay(string username, Guid artID)
        {
            return View(_artService.GetArt(artID));
        }

        [UserAuthFilter]
        [HttpGet]
        [Route("new")]
        public IActionResult UploadGet(ArtUploadModel art)
        {
            return View("/Views/Art/Upload.cshtml", art);
        }

        [UserAuthFilter]
        [HttpPost]
        [Route("new")]
        public IActionResult Upload(ArtUploadModel art)
        {
            List<string>? errors = null;
            if (ValidatorDTO.IsValid(art.ArtDTO, out errors))
            {
                if (art.Image != null) // TODO move to IFileStorageService or smth, this is very tmp and bad bc static files instead of storage
                {
                    Guid externalUUID = Guid.NewGuid();
                    string title = String.Concat(art.ArtDTO.Title.Where(char.IsLetterOrDigit));
                    string filePath = Path.Combine(
                        _tmpFolder, $"{Convert.ToString(externalUUID)}-{title}{Path.GetExtension(art.Image.FileName)}"
                        );
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        art.Image.CopyTo(fileStream);
                    }

                    art.ArtDTO.FilePath = filePath.Replace("wwwroot\\", "");
                    art.ArtDTO.ExternalUUID = externalUUID;
                    art.ArtDTO.Author = art.Username;
                    _artService.NewArt(art.ArtDTO);
                    return RedirectToAction("ArtDisplay", "Art", new { username = art.Username, artID = externalUUID });
                }
            }
            art.Errors = errors;
            return View(art);
        }

        [UserSpecificFilter]
        [HttpGet]
        [Route("{username}/gallery/{externalUUID:Guid}/update")]
        public IActionResult UpdateGet(string username, Guid externalUUID)
        {
            try
            {
                FormView<ArtDTO> pageModel = new FormView<ArtDTO>()
                {
                    Entity = _artService.GetArt(externalUUID)
                };
                return View("/Views/Art/Update.cshtml", pageModel);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [UserSpecificFilter]
        [HttpPost]
        [Route("{username}/gallery/{externalUUID:Guid}/update")]
        public IActionResult Update(string username, Guid externalUUID, FormView<ArtDTO> pageModel)
        {
            // TODO error handling
            _artService.UpdateArt(pageModel.Entity);
            return RedirectToAction("ArtDisplay", "Art", new { username = username, artID = externalUUID });
        }

        [UserSpecificFilter]
        [HttpGet]
        [Route("{username}/gallery/{externalUUID:Guid}/delete")]
        public IActionResult Delete(string username, Guid externalUUID)
        {
            // TODO confirmation and error handling
            _artService.DeleteArt(externalUUID);
            return RedirectToAction("Profile", "User", new { username});
        }
    }
}
