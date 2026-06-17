using PhantasmalARTdemo.Models.DTO;
using PhantasmalARTdemo.Models.Containers;
using PhantasmalARTdemo.Data.Models;
using PhantasmalARTdemo.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.ApiExplorer;

namespace PhantasmalARTdemo.Services.Implementations
{
    public class ArtService : IArtService
    {
        private AppDbContext _db;
        public ArtService(AppDbContext db)
        {
            _db = db;
        }

        // Soft delete
        public void DeleteArt(Guid externalGuid)
        {
            Art? artEntry = _db.Arts.FirstOrDefault(a => a.ExternalUUID == externalGuid);
            if (artEntry == null) throw new KeyNotFoundException($"Art with GUID {externalGuid} was not found on server");
            else if (artEntry.IsDeleted) throw new KeyNotFoundException($"Art with GUID {externalGuid} was already deleted");
            artEntry.IsDeleted = true;
            artEntry.UpdatedAt = DateTime.UtcNow;
            _db.SaveChanges();
        }

        public Page<ArtDTO> GetArt(int curPage)
        {
            Page<ArtDTO> page = new Page<ArtDTO>();
            page.CurPage = curPage;
            page.Items = new List<ArtDTO>();
            page.MaxPage = (int)Math.Ceiling((double)(_db.Arts.Count() / page.ItemsPerPage));

            List<Art> arts = _db.Arts.Where(art => !art.IsDeleted)
                                     .Include(art => art.User)
                                     .OrderByDescending(art => art.CreatedAt)
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
            Guid userID = _db.Users.FirstOrDefault(user => user.Username == username).ID;
            page.MaxPage = (int)Math.Ceiling((double)(_db.Arts.Where(art => art.UserID == userID).Count() / page.ItemsPerPage));

            List<Art> arts = _db.Arts.Where(art => art.UserID == userID && !art.IsDeleted)
                                     .Include(art => art.User)
                                     .OrderByDescending(art => art.CreatedAt)
                                     .Skip(curPage * page.ItemsPerPage)
                                     .Take(page.ItemsPerPage)
                                     .ToList();
            foreach (Art art in arts) page.Items.Add(FromEntity(art));

            return page;
        }

        public ArtDTO GetArt(Guid externalGuid)
        {
            Art? art = _db.Arts.Include(art => art.User).FirstOrDefault(art => art.ExternalUUID == externalGuid);
            if (art == null) throw new KeyNotFoundException($"Art with GUID {externalGuid} was not found on server");
            else if (art.IsDeleted) throw new KeyNotFoundException($"Art with GUID {externalGuid} was deleted");
            return FromEntity(art);

        }

        // Get preview art for user's profile
        //TODO - something with amount, make it configurable outside ig
        public List<ArtDTO> GetPreviewArt(string username, int amount = 5)
        {
            List<Art> query = _db.Arts.Where(
                art => 
                art.UserID == _db.Users.FirstOrDefault(user => user.Username == username).ID
                && !art.IsDeleted
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

        public void UpdateArt(ArtDTO art)
        {
            // TODO - add update for file
            Art? artEntry = _db.Arts.FirstOrDefault(a => a.ExternalUUID == art.ExternalUUID);
            if (artEntry == null) throw new KeyNotFoundException("Art with this GUID was not found on server");

            artEntry.Title = art.Title == null ? "Untitled" : art.Title;
            artEntry.Description = art.Description;
            artEntry.UpdatedAt = DateTime.UtcNow;
            if (art.FilePath != null) artEntry.FilePath = art.FilePath;
            _db.SaveChanges();
        }

        private ArtDTO FromEntity(Art art)
        {
            return new ArtDTO()
            {
                Title = art.Title,
                FilePath=art.FilePath,
                ExternalUUID = art.ExternalUUID,
                Author = art.User.Username,
                Description = art.Description,
                CreatedAt = art.CreatedAt,
                UpdatedAt = art.UpdatedAt,
                IsDeleted = art.IsDeleted
            };
        }
    }
}
