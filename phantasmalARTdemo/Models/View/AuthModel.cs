using phantasmalARTdemo.Models.DTO;

namespace phantasmalARTdemo.Models.View
{
    public class AuthModel // TODO move to FormModel
    {
        public UserDTO User { get; set; }
        public List<string>? Errors { get; set; }
    }
}
