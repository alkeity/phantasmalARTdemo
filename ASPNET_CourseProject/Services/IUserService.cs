using ASPNET_CourseProject.Data.Models;
using ASPNET_CourseProject.Models.DTO;

namespace ASPNET_CourseProject.Services
{
    public interface IUserService
    {
        
        public User GetById(Guid id);
        public User GetByEmail(string email);
        public UserDTO GetByUsername(string username);
        public UserDTO ConfirmUser(UserDTO userInfo, out List<string>? errors);
        public UserProfileDTO GetProfileByUsername(string username);
        public List<string>? Add(UserDTO user);

    }
}
