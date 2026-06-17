using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PhantasmalARTdemo.Data.Models
{
    public class UserProfile
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid ID { get; set; }

        [ForeignKey("User")]
        public required Guid UserID { get; set; }
        public User? User { get; set; }
        public string? Description { get; set; }

    }
}
