using phantasmalARTdemo.Models.DTO;

namespace phantasmalARTdemo.Models.View
{
    public class ArtUploadModel
    {
        public required ArtDTO ArtDTO { get; set; }
        public IFormFile? Image {  get; set; }
        public List<string>? Errors { get; set; }
    }
}
