using PhantasmalARTdemo.Data.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhantasmalARTdemo.Data.Models
{
    public class ArtComment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        [StringLength(2000, MinimumLength = 1)]
        public required string Text { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;

        [ForeignKey("User")]
        public required Guid UserID { get; set; }
        public User? User { get; set; }
        [ForeignKey("Art")]
        public required Guid ArtID { get; set; }
        public Art? Art { get; set; }
    }
}
