using ASPNET_CourseProject.Models.DTO;

namespace ASPNET_CourseProject.Models.View
{
    public class ProfileViewModel
    {
        public required UserDTO User { get; set; } = new UserDTO() { Username = "dummy" };
        public required UserProfileDTO Profile { get; set; } = new UserProfileDTO();
        public required List<ArtDTO> UserArt { get; set; } = new List<ArtDTO>();
    }
}
