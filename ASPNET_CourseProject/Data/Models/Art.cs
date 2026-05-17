using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET_CourseProject.Data.Models
{
    public class Art
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }
        public required Guid ExternalUUID { get; set; }
        public required string FilePath { get; set; }
        [StringLength(50, MinimumLength = 3)]
        public required string Title { get; set; }
        [StringLength(20000, MinimumLength = 3)]
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
        public bool IsDeleted { get; set; } = false;

        [ForeignKey("User")]
        public required Guid UserID { get; set; }
        public User? User { get; set; }
    }
}
