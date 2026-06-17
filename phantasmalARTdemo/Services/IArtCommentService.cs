using PhantasmalARTdemo.Models.Containers;
using PhantasmalARTdemo.Models.DTO;

namespace PhantasmalARTdemo.Services
{
    public interface IArtCommentService
    {
        public Page<ArtCommentDTO> GetComments(Guid artID, int curPage);
        public void NewComment(ArtCommentDTO comment);
        public void EditComment(ArtCommentDTO comment);
        public void DeleteComment(Guid id);
    }
}
