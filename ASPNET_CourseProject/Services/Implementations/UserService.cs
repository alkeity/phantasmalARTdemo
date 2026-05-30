using ASPNET_CourseProject.Data;
using ASPNET_CourseProject.Data.Models;
using ASPNET_CourseProject.Models.DTO;
using ASPNET_CourseProject.Validators;
using Microsoft.EntityFrameworkCore;
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
                newUser.RoleID = 1;
                EntityEntry<User> userEntry = _db.Users.Add(newUser);
                if (userEntry == null)
                {
                    errors = new List<string>();
                    errors.Add("Something went wrong with your registration.");
                    return errors;
                }

                UserProfile newProfile = new UserProfile() { User = userEntry.Entity, UserID = userEntry.Entity.ID };
                _db.UserProfiles.Add(newProfile);

                try
                {
                    _db.SaveChanges();
                }
                //catch (SqlException)
                //{
                //    errors = new List<string>();
                //    errors.Add("Something went wrong with registration.");
                //    return errors;
                //}
                catch (DbUpdateException)
                {
                    errors = new List<string>();
                    errors.Add("E-mail or username you want to use is already taken.");
                    return errors;
                }
            }
            return errors;
        }

        public UserDTO ConfirmUser(UserDTO userInfo, out List<string>? errors)
        {
            errors = new List<string>();
            User? user = _db.Users.FirstOrDefault(u => u.Email == userInfo.Email);
            if (user == null) errors.Add("The email you entered does not belong to any account.");
            else if (user.Password != userInfo.Password) errors.Add("The password you entered is incorrect.");
            else if (user.IsDeleted) errors.Add("The email you entered belongs to deleted account. Contact administration to restore it.");
            else if (user.IsBanned) errors.Add("The email you entered belongs to banned account. Contact administration to object it.");
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

        public UserDTO? GetByUsername(string username)
        {
            Console.WriteLine($"USER: Searching for user: {username}");
            User? user = _db.Users.FirstOrDefault(u => u.Username == username);
            if (user == null) return null;
            return ConvertToDTO(user);
        }

        public UserProfileDTO GetProfileByUsername(string username)
        {
            UserProfile? userProfile = _db.UserProfiles.FirstOrDefault(
                profile => profile.UserID == _db.Users.FirstOrDefault(user => user.Username == username).ID
                );
            if (userProfile == null) throw new KeyNotFoundException($"USER: Profile for user {username} was not found");
            return ConvertToDTO(userProfile);

        }

        public void Update(UserDTO userDTO)
        {
            // TODO pass hash
            // find user and update it

            User user = _db.Users.FirstOrDefault(u => u.Username == userDTO.Username);
            if (user == null) throw new KeyNotFoundException($"User {userDTO.Username} not found");

            user.Username = userDTO.Username;
            user.Password = userDTO.Password;
            user.Email = userDTO.Email;
            user.IsDeleted = userDTO.IsDeleted;
            user.IsBanned = userDTO.IsBanned;
            user.UpdatedAt = DateTime.UtcNow;
            _db.SaveChanges();
        }

        public void UpdateProfile(string username, UserProfileDTO profile)
        {
            UserProfile userProfile = _db.UserProfiles.FirstOrDefault(
                                      pr => pr.UserID == _db.Users.FirstOrDefault(u => u.Username == username).ID
                );
            if (userProfile == null) throw new KeyNotFoundException($"Profile for user {username} was not found");
            userProfile.Description = profile.Description;
            _db.SaveChanges();
        }

        private User ConvertFromDTO(UserDTO dto)
        {
            return new User()
            {
                Username = dto.Username,
                Password = dto.Password,
                Email = dto.Email,
                RoleID = dto.Role,
                CreatedAt = dto.CreatedAt,
                UpdatedAt = dto.UpdatedAt,
                IsDeleted = dto.IsDeleted,
                IsBanned = dto.IsBanned
            };
        }

        private UserDTO ConvertToDTO(User user)
        {
            return new UserDTO()
            {
                Username = user.Username,
                Password = user.Password,
                Email = user.Email,
                Role = user.RoleID,
                CreatedAt = user.CreatedAt,
                UpdatedAt = user.UpdatedAt,
                IsDeleted = user.IsDeleted,
                IsBanned = user.IsBanned
            };
        }

        private UserProfileDTO ConvertToDTO(UserProfile profile)
        {
            return new UserProfileDTO()
            {
                Description = profile.Description
            };
        }
    }
}
