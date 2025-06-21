using ASPNET_CourseProject.Data.Models;

namespace ASPNET_CourseProject.Models.DTO
{
    public class UserProfileDTO
    {
        public UserDTO? User { get; set; }
        public string? Description { get; set; }
    }
}
