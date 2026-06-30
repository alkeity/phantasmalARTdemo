using PhantasmalARTdemo.Data.Models;
using PhantasmalARTdemo.Filters;
using PhantasmalARTdemo.Models.Containers;
using PhantasmalARTdemo.Models.DTO;
using PhantasmalARTdemo.Models.View;
using PhantasmalARTdemo.Services;
using PhantasmalARTdemo.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PhantasmalARTdemo.Controllers
{
    public class ArtController : Controller
    {
        private IArtService _artService;
        private IArtCommentService _artCommentService;
        private IUserService _userService;
        private IStorageService _storageService;
        private IHttpContextAccessor _contextAccessor;
        private readonly string _storagePath;

        public ArtController(
            IArtService artService, IArtCommentService artCommentService, IUserService userService, 
            IStorageService storageService, IHttpContextAccessor contextAccessor, 
            IConfiguration config)
        {
            _artService = artService;
            _artCommentService = artCommentService;
            _userService = userService;
            _storageService = storageService;
            _contextAccessor = contextAccessor;
            _storagePath = Path.Combine(
                config.GetSection("ObjectStorage").GetValue<string>("Endpoint"),
                config.GetSection("ObjectStorage").GetValue<string>("BucketName")
                );
        }

        [HttpGet]
        [Route("{username}/gallery")]
        public IActionResult UserGallery(string username, int page = 0)
        {
            // TODO check if user exists, redirect to 404 if not
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
            ArtDTO art;
            try
            {
                art = _artService.GetArt(artID);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound();
            }
            ArtDisplayView artView = new ArtDisplayView()
            {
                Art = art
            };
            return View(artView);
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
            // TODO rewrite this controller
            // also i feel like there's too much stuff going on for a controller??
            string? username = _contextAccessor.HttpContext.Session.GetString("UserName");
            List<string>? errors = null;
            if (ValidatorDTO.IsValid(art.ArtDTO, out errors))
            {
                Console.WriteLine("Art valid");
                if (art.Image != null)
                {
                    Console.WriteLine("Img not null");
                    Guid externalUUID = Guid.NewGuid();
                    string title = String.Concat(art.ArtDTO.Title.Where(char.IsLetterOrDigit));
                    string fileName = $"{Convert.ToString(externalUUID)}-{title}{Path.GetExtension(art.Image.FileName)}";
                    using (Stream stream = art.Image.OpenReadStream())
                    {
                        _storageService.UploadArt(fileName, stream);
                    }
                    // TODO there must be a better way to keep the art paths? but so far that's the only way i found,
                    // everything else is temporary paths
                    // maybe should check the way to pull art out of the storage with some info, not by actual path
                    art.ArtDTO.FilePath = Path.Combine($"http://{_storagePath}", "art", fileName);
                    art.ArtDTO.ExternalUUID = externalUUID;
                    art.ArtDTO.Author = username;
                    _artService.NewArt(art.ArtDTO);
                    return RedirectToAction("ArtDisplay", "Art", new { username = username, artID = externalUUID });
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
                ArtUploadModel pageModel = new ArtUploadModel()
                {
                    ArtDTO = _artService.GetArt(externalUUID)
                };
                return View("/Views/Art/Update.cshtml", pageModel);
            }
            catch (KeyNotFoundException)
            {
                return NotFound();
            }
        }

        [HttpPost]
        [Route("{username}/gallery/{externalUUID:Guid}/update")]
        public IActionResult Update(string username, Guid externalUUID, ArtUploadModel art)
        {
            List<string>? errors = null;
            if (ValidatorDTO.IsValid(art.ArtDTO, out errors))
            {
                Console.WriteLine("Art update valid");
                if (art.Image != null)
                {
                    Console.WriteLine("Img not null");
                    string title = String.Concat(art.ArtDTO.Title.Where(char.IsLetterOrDigit));
                    string fileName = $"{Convert.ToString(externalUUID)}-{title}{Path.GetExtension(art.Image.FileName)}";
                    using (Stream stream = art.Image.OpenReadStream())
                    {
                        _storageService.UploadArt(fileName, stream);
                    }
                    art.ArtDTO.FilePath = Path.Combine($"http://{_storagePath}", "art", fileName);
                }
                art.ArtDTO.ExternalUUID = externalUUID;
                art.ArtDTO.Author = username;
                _artService.UpdateArt(art.ArtDTO);
                return RedirectToAction("ArtDisplay", "Art", new { username = username, artID = externalUUID });
            }
            art.Errors = errors;
            return View(art);
        }

        [UserSpecificFilter]
        [HttpGet]
        [Route("{username}/gallery/{externalUUID:Guid}/delete")]
        public IActionResult Delete(string username, Guid externalUUID)
        {
            // TODO confirmation and error handling
            try
            {
                _artService.DeleteArt(externalUUID);
                return RedirectToAction("Profile", "User", new { username });
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound();
            }
        }
    }
}
