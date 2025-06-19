using ASPNET_CourseProject.Data.Models;
using ASPNET_CourseProject.Models.DTO;

namespace ASPNET_CourseProject.Services
{
    public interface IUserService
    {
        
        public User GetById(Guid id);
        public User GetByEmail(string email);
        public User GetByUsername(string username);
        /*
         * @return 0 if user exists and password correct,
         * 1 if user does not exists, 2 if password incorrect
         */
        public int ConfirmUser(UserDTO user);
        public bool Add(UserDTO user);

    }
}
