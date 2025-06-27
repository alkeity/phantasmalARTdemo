using ASPNET_CourseProject.Models.DTO;

namespace ASPNET_CourseProject.Models.View
{
    public class ProfileModel
    {
        public UserDTO User { get; set; }
        public UserProfileDTO Profile { get; set; }
        public List<ArtDTO> UserArt { get; set; }
    }
}
