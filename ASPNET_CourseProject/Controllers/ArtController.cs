using ASPNET_CourseProject.Models.DTO;
using ASPNET_CourseProject.Models.View;
using ASPNET_CourseProject.Services;
using ASPNET_CourseProject.Services.Implementations;
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
        [Route("{username}/gallery/{artTitle}-{artID:Guid}")]
        public IActionResult ArtDisplay(string username, string artTitle, Guid artID)
        {
            return View();
        }

        [HttpGet]
        [Route("new")]
        public IActionResult UploadGet(ArtUploadModel art)
        {
            return View("/Views/Art/Upload.cshtml", art);
        }

        [HttpPost]
        [Route("new")]
        public IActionResult Upload(ArtUploadModel art)
        {
            List<string>? errors = null;
            if (ModelState.IsValid && ValidatorDTO.IsValid(art.ArtDTO, out errors))
            {
                if (art.Image != null) // TODO move to different method
                {
                    Guid externalUUID = Guid.NewGuid();
                    string title = String.Concat(art.ArtDTO.Title.Where(char.IsLetterOrDigit));
                    string filePath = Path.Combine(_tmpFolder, $"{Convert.ToString(externalUUID)}-{title}");
                    Console.WriteLine($"Path: {filePath}");
                    using (FileStream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        art.Image.CopyTo(fileStream);
                    }

                    art.ArtDTO.FilePath = filePath;
                    art.ArtDTO.ExternalUUID = externalUUID;
                    art.ArtDTO.Author = _userService.GetByUsername(art.Username);
                    _artService.NewArt(art.ArtDTO);
                    return RedirectToAction("Home", "Index"); // TODO redirect to uploaded art page
                }
            }
            art.Errors = errors;
            return View(art);
        }
    }
}
