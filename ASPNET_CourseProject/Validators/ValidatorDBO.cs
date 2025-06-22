using ASPNET_CourseProject.Data.Models;
using ASPNET_CourseProject.Models.DTO;

namespace ASPNET_CourseProject.Validators
{
    public static class ValidatorDBO
    {
        public static bool IsValid(UserDTO user, out List<string>? errors)
        {
            errors = new List<string>();

            if (string.IsNullOrEmpty(user.Username))
            {
                errors.Add("Username cannot be empty");
            }
            else if (user.Username.Length < 3 || user.Username.Length > 20)
            {
                errors.Add("Username must be between 3 and 20 characters long");
            }

            if (string.IsNullOrEmpty(user.Password))
            {
                errors.Add("Password cannot be empty");
            }
            else if (user.Password.Length < 7 || user.Password.Length > 20)
            {
                errors.Add("Password must be between 7 and 20 characters long");
            }

            if (string.IsNullOrEmpty(user.Email))
            {
                errors.Add("E-mail address cannot be empty");
            }

            if (errors.Count <= 0) errors = null;
            return errors == null;
        }

        public static bool IsValid(ArtDTO art, out List<string>? errors)
        {
            errors = new List<string>();

            if (string.IsNullOrEmpty(art.Title))
            {
                errors.Add("Title cannot be empty");
            }
            else if (art.Title.Length < 3 || art.Title.Length > 50)
            {
                errors.Add("Title must be between 3 and 50 characters long");
            }

            if (errors.Count <= 0) errors = null;
            return errors == null;
        }
    }
}
