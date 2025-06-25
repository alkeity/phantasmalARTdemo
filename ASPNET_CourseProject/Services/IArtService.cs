using ASPNET_CourseProject.Models.Containers;
using ASPNET_CourseProject.Models.DTO;

namespace ASPNET_CourseProject.Services
{
    public interface IArtService
    {
        public Page<ArtDTO> GetArt(int curPage);
        public Page<ArtDTO> GetArt(int curPage, string username);
        public void NewArt(ArtDTO art);
    }
}
