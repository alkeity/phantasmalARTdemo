using PhantasmalARTdemo.Data.Models;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhantasmalARTdemo.Data.Models
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        [Required]
        [StringLength(20, MinimumLength = 3)]
        public required string Username { get; set; } // unique
        [Required]
        [EmailAddress]
        public required string Email { get; set; } // unique
        [Required]
        [PasswordPropertyText]
        [StringLength(20, MinimumLength = 7)]
        public required string Password { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt {  get; set; } = DateTime.UtcNow;
        public DateTime LastLogin {  get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;
        public bool IsBanned { get; set; } = false;

        [ForeignKey("Role")]
        public required byte RoleID { get; set; } = 1;
        public Role? UserRole { get; set; }
        public IList<Art> UserArt { get; set; } = [];
        public IList<ArtComment> Comments { get; set; } = [];
        public UserProfile? Profile { get; set; }
    }
}
