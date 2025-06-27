using ASPNET_CourseProject.Models.Containers;
using ASPNET_CourseProject.Models.DTO;

namespace ASPNET_CourseProject.Models.View
{
    public class UserGalleryModel
    {
        public UserDTO User { get; set; }
        public Page<ArtDTO> Art {  get; set; }
    }
}
