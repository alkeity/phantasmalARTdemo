using System.ComponentModel.DataAnnotations;

namespace ASPNET_CourseProject.Models.DTO
{
    public class UserDTO
    {
        public string? Username { get; set; }
        public byte Role {  get; set; }
        [EmailAddress]
        public string? Email { get; set; }
        public string? Password { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsBanned { get; set; }
    }
}
