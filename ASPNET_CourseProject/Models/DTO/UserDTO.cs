namespace ASPNET_CourseProject.Models.DTO
{
    public class UserDTO
    {
        public string? Username { get; set; }
        public string? Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
