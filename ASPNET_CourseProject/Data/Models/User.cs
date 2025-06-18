using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET_CourseProject.Data.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required Guid ID { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt {  get; set; } = DateTime.Now;

        public List<Art> UserArt { get; set; } = new List<Art>();
    }
}
