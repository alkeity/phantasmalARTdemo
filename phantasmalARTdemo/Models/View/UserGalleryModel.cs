using phantasmalARTdemo.Models.Containers;
using phantasmalARTdemo.Models.DTO;

namespace phantasmalARTdemo.Models.View
{
    public class UserGalleryModel
    {
        public UserDTO User { get; set; }
        public Page<ArtDTO> Art {  get; set; }
    }
}
