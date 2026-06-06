using phantasmalARTdemo.Models.Containers;
using phantasmalARTdemo.Models.DTO;

namespace phantasmalARTdemo.Services
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
