using System.Threading.Tasks;
using PhantasmalARTdemo.Migrations;
using PhantasmalARTdemo.Models.Containers;
using PhantasmalARTdemo.Models.DTO;
using PhantasmalARTdemo.Services;
using Microsoft.AspNetCore.Mvc;
using Minio;

namespace PhantasmalARTdemo.Controllers
{
    public class HomeController : Controller
    {
        private IArtService _artService;
        private readonly IMinioClient _minioClient;

        public HomeController(IArtService artService, IMinioClient minioClient)
        {
            _artService = artService;
            _minioClient = minioClient;
        }
        public async Task<IActionResult> Index(int page = 0)
        {
            page = Math.Clamp(page, 0, int.MaxValue);
            Page<ArtDTO> model = _artService.GetArt(page);

            try
            {
                Console.WriteLine("Running example for API: ListBucketsAsync");
                var list = await _minioClient.ListBucketsAsync().ConfigureAwait(false);
                foreach (var bucket in list.Buckets) Console.WriteLine($"{bucket.Name} {bucket.CreationDateDateTime}");
                Console.WriteLine();
            }
            catch (Exception e)
            {
                Console.WriteLine($"[Bucket]  Exception: {e}");
            }
            return View(model);
        }

        [Route("terms-and-conditions")]
        public IActionResult TermsConditions()
        {
            return View();
        }
    }
}
