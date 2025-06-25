using ASPNET_CourseProject.Models.DTO;

namespace ASPNET_CourseProject.Models.View
{
    public class ArtUploadModel
    {
        public required ArtDTO ArtDTO { get; set; }
        public required string Username { get; set; }
        public required IFormFile Image {  get; set; }
        public List<string>? Errors { get; set; }
    }
}
