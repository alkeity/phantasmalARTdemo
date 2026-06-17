using PhantasmalARTdemo.Models.DTO;

namespace PhantasmalARTdemo.Models.View
{
    public class AuthModel // TODO move to FormModel
    {
        public UserDTO User { get; set; }
        public List<string>? Errors { get; set; }
    }
}
