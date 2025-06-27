using ASPNET_CourseProject.Models.DTO;

namespace ASPNET_CourseProject.Models.View
{
    public class AuthModel // TODO move to FormModel
    {
        public UserDTO User { get; set; }
        public List<string>? Errors { get; set; }
    }
}
