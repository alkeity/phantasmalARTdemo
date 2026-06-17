using PhantasmalARTdemo.Models.Containers;
using PhantasmalARTdemo.Models.DTO;

namespace PhantasmalARTdemo.Models.View
{
    public class UserGalleryModel
    {
        public UserDTO User { get; set; }
        public Page<ArtDTO> Art {  get; set; }
    }
}
