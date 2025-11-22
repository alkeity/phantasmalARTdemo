using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET_CourseProject.Data.Models
{
    public class Role
    {
        [Key]
        public byte ID { get; set; }
        [Required]
        public required string RoleName { get; set; }

        public IList<User> Users { get; set; } = [];
    }
}
