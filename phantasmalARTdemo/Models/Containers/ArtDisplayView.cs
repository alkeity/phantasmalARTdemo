using PhantasmalARTdemo.Models.DTO;

namespace PhantasmalARTdemo.Models.Containers
{
    public class ArtDisplayView
    {
        public required ArtDTO Art { get; set; }
        public Page<ArtCommentDTO>? Comments { get; set; } = new Page<ArtCommentDTO>();
        public BaseFormView<ArtCommentDTO> CommentForm { get; set; } = new BaseFormView<ArtCommentDTO>() 
        { Entity = new ArtCommentDTO() };
    }
}
