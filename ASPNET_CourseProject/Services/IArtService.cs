using ASPNET_CourseProject.Models.Containers;
using ASPNET_CourseProject.Models.DTO;

namespace ASPNET_CourseProject.Services
{
    public interface IArtService
    {
        public Page<ArtDTO> GetArt(int curPage);
        public Page<ArtDTO> GetArt(int curPage, string username);
        public ArtDTO GetArt(Guid externalGuid);
        public List<ArtDTO> GetPreviewArt(string username, int amount = 5);
        public void NewArt(ArtDTO art);
        public void UpdateArt(ArtDTO art);
        public void DeleteArt(Guid externalGuid);
    }
}
