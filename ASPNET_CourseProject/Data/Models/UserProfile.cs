using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET_CourseProject.Data.Models
{
    public class UserProfile
    {
        [Key]
        public Guid ID { get; set; }

        [ForeignKey("User")]
        public required Guid UserID { get; set; }
        public required User User { get; set; }
        public string? Description { get; set; }

    }
}
