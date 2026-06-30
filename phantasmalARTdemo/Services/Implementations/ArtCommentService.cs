using Microsoft.EntityFrameworkCore;
using PhantasmalARTdemo.Data;
using PhantasmalARTdemo.Data.Models;
using PhantasmalARTdemo.Models.Containers;
using PhantasmalARTdemo.Models.DTO;

namespace PhantasmalARTdemo.Services.Implementations
{
    public class ArtCommentService : IArtCommentService
    {
        private AppDbContext _db;
        public ArtCommentService(AppDbContext db)
        {
            _db = db;
        }

        public void DeleteComment(Guid id)
        {
            ArtComment? comment = _db.ArtComments.FirstOrDefault(c => c.ID == id);
            if (comment == null) throw new KeyNotFoundException($"Comment with GUID {id} was not found on server");
            else if (comment.IsDeleted) throw new KeyNotFoundException($"Comment with GUID {id} was already deleted");
            comment.IsDeleted = true;
            comment.UpdatedAt = DateTime.UtcNow;
            _db.SaveChanges();
        }

        public void EditComment(ArtCommentDTO comment)
        {
            ArtComment? cmt = _db.ArtComments.FirstOrDefault(c => c.ID == comment.ID);
            if (cmt == null) throw new KeyNotFoundException($"Comment with GUID {comment.ID} was not found on server");

            cmt.Text = comment.Text != null ? comment.Text : "";
            cmt.UpdatedAt = DateTime.UtcNow;

            _db.SaveChanges();
        }

        // TODO configurable from outside default amount
        public Page<ArtCommentDTO> GetComments(Guid artID, int curPage, int amount = 10)
        {
            Page<ArtCommentDTO> page = new Page<ArtCommentDTO>();
            page.ItemsPerPage = amount;
            page.CurPage = curPage;
            page.Items = new List<ArtCommentDTO>();

            Guid actualArtID = _db.Arts.FirstOrDefault(art => art.ExternalUUID == artID).ID;

            List<ArtComment> comments = _db.ArtComments.Where(c => c.ArtID == actualArtID && !c.IsDeleted)
                                                       .Include(c => c.User)
                                                       .OrderByDescending(c => c.CreatedAt)
                                                       .Skip(curPage * page.ItemsPerPage)
                                                       .Take(page.ItemsPerPage)
                                                       .ToList();
            foreach (ArtComment comment in comments) page.Items.Add(FromEntity(comment));

            return page;
        }

        // add new entry to database
        // artID - external GUID, not primary key
        public void NewComment(ArtCommentDTO comment, Guid artID)
        {
            _db.ArtComments.Add(
                new ArtComment()
                {
                    Text = comment.Text,
                    ArtID = _db.Arts.FirstOrDefault(art => art.ExternalUUID == artID).ID,
                    UserID = _db.Users.FirstOrDefault(user => user.Username == comment.Author).ID
                }
            );
            _db.SaveChanges();
        }

        private ArtCommentDTO FromEntity(ArtComment entity) {
            return new ArtCommentDTO()
            {
                ID = entity.ID,
                Text = entity.Text,
                Author = entity.User.Username,
                CreatedAt = entity.CreatedAt,
                UpdatedAt = entity.UpdatedAt,
                IsDeleted = entity.IsDeleted
            };
        }
    }
}
