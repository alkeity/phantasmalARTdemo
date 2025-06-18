namespace ASPNET_CourseProject.Models.DTO
{
    public class ArtDTO
    {
        public Guid ID { get; set; }
        public string? FilePath { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
