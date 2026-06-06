using phantasmalARTdemo.Models.DTO;

namespace phantasmalARTdemo.Models.Containers
{
    public class Page<T>
    {
        public List<T>? Items { get; set; }
        public int CurPage { get; set; }
        public int MaxPage { get; set; }
        public int ItemsPerPage { get; set; } = 15;
    }
}
