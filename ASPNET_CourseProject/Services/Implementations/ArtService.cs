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
            page.MaxPage = (int)Math.Ceiling((double)(_db.Arts.Count() / page.ItemsPerPage));

            List<Art> arts = _db.Arts.OrderByDescending(art => art.CreatedAt)
                                     .Skip(curPage * page.ItemsPerPage)
                                     .Take(page.ItemsPerPage)
                                     .ToList();


            foreach (Art art in arts) page.Items.Add(FromEntity(art));

            return page;
        }

        public Page<ArtDTO> GetArt(int curPage, string username)
        {
            Page<ArtDTO> page = new Page<ArtDTO>();
            page.CurPage = curPage;
            page.Items = new List<ArtDTO>();
            page.MaxPage = (int)Math.Ceiling((double)(_db.Arts.Count() / page.ItemsPerPage));
            Guid userID = _db.Users.FirstOrDefault(user => user.Username == username).ID;

            List<Art> arts = _db.Arts.Where(art => art.UserID == userID)
                                     .OrderByDescending(art => art.CreatedAt)
                                     .Take(page.ItemsPerPage)
                                     .ToList();
            foreach (Art art in arts) page.Items.Add(FromEntity(art));

            return page;
        }

        public ArtDTO GetArt(Guid externalGuid)
        {
            Art? art = _db.Arts.FirstOrDefault(art => art.ExternalUUID == externalGuid);
            if (art == null) throw new FileNotFoundException("Art with this GUID was not found on server");
            return FromEntity(art);

        }

        public List<ArtDTO> GetPreviewArt(string username, int amount = 5)
        {
            List<Art> query = _db.Arts.Where(
                art => art.UserID == _db.Users.FirstOrDefault(user => user.Username == username).ID
                ).OrderByDescending(art => art.CreatedAt).Take(amount).ToList();

            List<ArtDTO> result = new List<ArtDTO>();

            foreach (Art art in query) result.Add(FromEntity(art));

            return result;
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
                    UserID = _db.Users.FirstOrDefault(user => user.Username == art.Author).ID
                }
                );
            _db.SaveChanges();
        }

        private ArtDTO FromEntity(Art art)
        {
            return new ArtDTO()
            {
                Title = art.Title,
                FilePath=art.FilePath,
                ExternalUUID = art.ExternalUUID,
                Author = _db.Users.FirstOrDefault(user => user.ID == art.UserID).Username,
                Description = art.Description,
                CreatedAt = art.CreatedAt,
                UpdatedAt = art.UpdatedAt
            };
        }
    }
}
