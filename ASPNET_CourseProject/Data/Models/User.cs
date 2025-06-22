using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET_CourseProject.Data.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        [StringLength(20, MinimumLength = 3)]
        public required string Username { get; set; } // unique
        [EmailAddress]
        public required string Email { get; set; } // unique
        [StringLength(20, MinimumLength = 7)]
        public required string Password { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt {  get; set; } = DateTime.Now;
        public DateTime LastLogin {  get; set; } = DateTime.Now;

        public List<Art> UserArt { get; set; } = new List<Art>();
        public UserProfile? Profile { get; set; }
    }
}
