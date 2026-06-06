using phantasmalARTdemo.Models.DTO;

namespace phantasmalARTdemo.Models.View
{
    public class ProfileModel
    {
        public UserDTO User { get; set; }
        public UserProfileDTO Profile { get; set; }
        public List<ArtDTO> UserArt { get; set; }
    }
}
