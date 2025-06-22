using ASPNET_CourseProject.Models.DTO;

namespace ASPNET_CourseProject.Models.View
{
    public class AuthViewModel
    {
        public UserDTO User { get; set; }
        public List<string>? Errors { get; set; }
    }
}
