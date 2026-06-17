using PhantasmalARTdemo.Data.Models;
using PhantasmalARTdemo.Models.DTO;

namespace PhantasmalARTdemo.Services
{
    public interface IUserService
    {
        public User GetById(Guid id);
        public User GetByEmail(string email);
        public UserDTO? GetByUsername(string username);
        public UserDTO ConfirmUser(UserDTO userInfo, out List<string>? errors);
        public List<string>? Add(UserDTO user);
        public UserProfileDTO GetProfileByUsername(string username);
        public void UpdateProfile(string username, UserProfileDTO profile);
        public void Update(UserDTO user);

    }
}
