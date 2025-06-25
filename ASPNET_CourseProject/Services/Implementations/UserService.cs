using ASPNET_CourseProject.Data;
using ASPNET_CourseProject.Data.Models;
using ASPNET_CourseProject.Models.DTO;
using ASPNET_CourseProject.Validators;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace ASPNET_CourseProject.Services.Implementations
{
    public class UserService : IUserService
    {
        AppDbContext _db;

        public UserService(AppDbContext db)
        {
            _db = db;
        }

        public List<string>? Add(UserDTO user)
        {
            List<string>? errors = null;
            if (ValidatorDTO.IsValid(user, out errors))
            {
                User newUser = ConvertFromDTO(user);
                EntityEntry<User> userEntry = _db.Users.Add(newUser);
                if (userEntry == null)
                {
                    errors = new List<string>();
                    errors.Add("Something went wrong with registration.");
                    return errors;
                }
                UserProfile newProfile = new UserProfile() { User = userEntry.Entity, UserID = userEntry.Entity.ID };
                _db.UserProfiles.Add(newProfile);
                _db.SaveChanges();
            }
            return errors;
        }

        public UserDTO ConfirmUser(UserDTO userInfo, out List<string>? errors)
        {
            errors = new List<string>();
            User? user = _db.Users.FirstOrDefault(u => u.Email == userInfo.Email);
            if (user == null) errors.Add("The email you entered does not belong to any account.");
            else if (user.Password != userInfo.Password) errors.Add("The password you entered is incorrect.");
            else
            {
                errors = null;
                userInfo.Username = user.Username;
            }
            return userInfo;
        }

        public User GetByEmail(string email)
        {
            throw new NotImplementedException();
        }

        public User GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public UserDTO GetByUsername(string username)
        {
            User? user = _db.Users.FirstOrDefault(u => u.Username == username);
            if (user == null) throw new KeyNotFoundException("User with this username was not found.");
            return ConvertToDTO(user);
        }

        private User ConvertFromDTO(UserDTO dto)
        {
            return new User()
            {
                Email = dto.Email,
                Username = dto.Username,
                Password = dto.Password
            };
        }

        private UserDTO ConvertToDTO(User user)
        {
            return new UserDTO()
            {
                Username = user.Username,
                Password = user.Password,
                Email = user.Email
            };
        }
    }
}
