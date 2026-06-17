using PhantasmalARTdemo.Models.DTO;

namespace PhantasmalARTdemo.Models.View
{
    public class ArtUploadModel
    {
        public required ArtDTO ArtDTO { get; set; }
        public IFormFile? Image {  get; set; }
        public List<string>? Errors { get; set; }
    }
}
