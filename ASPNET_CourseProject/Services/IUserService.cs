using ASPNET_CourseProject.Data.Models;

namespace ASPNET_CourseProject.Services
{
    public interface IUserService
    {
        public User GetById(Guid id);
    }
}
