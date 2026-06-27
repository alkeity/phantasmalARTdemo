using PhantasmalARTdemo.Models.Containers;
using PhantasmalARTdemo.Models.DTO;

namespace PhantasmalARTdemo.Services
{
    public interface IArtCommentService
    {
        public Page<ArtCommentDTO> GetComments(Guid artID, int curPage, int amount);
        public void NewComment(ArtCommentDTO comment, Guid artID);
        public void EditComment(ArtCommentDTO comment);
        public void DeleteComment(Guid id);
    }
}
