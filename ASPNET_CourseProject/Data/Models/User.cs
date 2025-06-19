using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET_CourseProject.Data.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required Guid ID { get; set; }
        public required string Username { get; set; } // unique
        public required string Email { get; set; } // unique
        public required string Password { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt {  get; set; } = DateTime.Now;
        public DateTime LastLogin {  get; set; } = DateTime.Now;

        public List<Art> UserArt { get; set; } = new List<Art>();
    }
}

// TODO: follows, favourites, profile views
