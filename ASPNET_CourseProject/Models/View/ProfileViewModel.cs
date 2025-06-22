using ASPNET_CourseProject.Models.DTO;

namespace ASPNET_CourseProject.Models.View
{
    public class ProfileViewModel
    {
        public UserDTO User { get; set; }
        public UserProfileDTO Profile { get; set; } = new UserProfileDTO();
        public List<ArtDTO> UserArt { get; set; } = new List<ArtDTO>();
    }
}
