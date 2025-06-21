using ASPNET_CourseProject.Models.DTO;

namespace ASPNET_CourseProject.Validators
{
    public static class ValidatorDBO
    {
        public static bool IsValid(UserDTO user, out Dictionary<string, string>? errors)
        {
            errors = null;
            return true;
        }

        public static bool IsValid(ArtDTO art, out Dictionary<string, string>? errors)
        {
            errors = null;
            return true;
        }
    }
}
