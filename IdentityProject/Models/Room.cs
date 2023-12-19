using System.ComponentModel.DataAnnotations;

namespace IdentityProject.Models
{
    public class Room
    {
        [Key]
        [Required]
        [StringLength(10)]
        public string r_id { get; set; }

        [Range(0, int.MaxValue)]
        public int r_capacity { get; set; }
    }
}
