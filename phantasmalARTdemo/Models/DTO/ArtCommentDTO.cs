namespace PhantasmalARTdemo.Models.DTO
{
    public class ArtCommentDTO
    {
        public Guid ID { get; set; }
        public string? Text { get; set; }
        public string? Author { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }

    }
}
