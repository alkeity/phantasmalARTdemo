using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ASPNET_CourseProject.Data.Models
{
    public class Art
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public required Guid ID { get; set; }
        public required string FilePath { get; set; }
        [StringLength(50, MinimumLength = 3)]
        public required string Title { get; set; }
        [StringLength(2048, MinimumLength = 3)]
        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        [ForeignKey("User")]
        public required Guid UserID { get; set; }
        public required User User { get; set; }
    }
}
