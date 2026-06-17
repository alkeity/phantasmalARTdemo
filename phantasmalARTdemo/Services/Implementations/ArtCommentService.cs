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
            if (comment == null) throw new KeyNotFoundException($"Art with GUID {id} was not found on server");
            else if (comment.IsDeleted) throw new KeyNotFoundException($"Art with GUID {id} was already deleted");
            comment.IsDeleted = true;
            comment.UpdatedAt = DateTime.UtcNow;
            _db.SaveChanges();
        }

        public void EditComment(ArtCommentDTO comment)
        {
            throw new NotImplementedException();
        }

        public Page<ArtCommentDTO> GetComments(Guid artID, int curPage)
        {
            throw new NotImplementedException();
        }

        public void NewComment(ArtCommentDTO comment)
        {
            throw new NotImplementedException();
        }
    }
}
