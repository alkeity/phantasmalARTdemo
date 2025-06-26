using ASPNET_CourseProject.Models.DTO;
using ASPNET_CourseProject.Models.Containers;
using ASPNET_CourseProject.Data.Models;
using ASPNET_CourseProject.Data;

namespace ASPNET_CourseProject.Services.Implementations
{
    public class ArtService : IArtService
    {
        private AppDbContext _db;
        public ArtService(AppDbContext db)
        {
            _db = db;
        }
        public Page<ArtDTO> GetArt(int curPage)
        {
            Page<ArtDTO> page = new Page<ArtDTO>();
            page.CurPage = curPage;
            page.Items = new List<ArtDTO>();

            List<Art> arts = _db.Arts.ToList();
            page.MaxPage = (int)Math.Ceiling((double)(arts.Count / page.ItemsPerPage));
            int start = curPage * page.ItemsPerPage;

            for (int i = start; i < start + page.ItemsPerPage && i < arts.Count; i++)
            {
                page.Items.Add(
                    new ArtDTO()
                    {
                        Title = arts[i].Title,
                        FilePath = arts[i].FilePath
                    }
                );
            }
            return page;
        }

        public Page<ArtDTO> GetArt(int curPage, string username)
        {
            throw new NotImplementedException();
        }

        public void NewArt(ArtDTO art)
        {
            _db.Arts.Add(
                new Art()
                {
                    ExternalUUID = art.ExternalUUID,
                    FilePath = art.FilePath,
                    Title = art.Title,
                    Description = art.Description,
                    UserID = _db.Users.FirstOrDefault(user => user.Username == art.Author.Username).ID
                }
                );
            _db.SaveChanges();
        }
    }
}
