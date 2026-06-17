using PhantasmalARTdemo.Models.DTO;

namespace PhantasmalARTdemo.Models.View
{
    public class ProfileModel
    {
        public UserDTO User { get; set; }
        public UserProfileDTO Profile { get; set; }
        public List<ArtDTO> UserArt { get; set; }
    }
}
