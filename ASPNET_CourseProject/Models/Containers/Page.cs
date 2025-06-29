using ASPNET_CourseProject.Models.DTO;

namespace ASPNET_CourseProject.Models.Containers
{
    public class Page<T>
    {
        public List<T>? Items { get; set; }
        public int CurPage { get; set; }
        public int MaxPage { get; set; }
        public int ItemsPerPage { get; set; } = 15;
    }
}
