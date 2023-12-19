using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IdentityProject.Models
{
    public class Discount
    {
        [Key]
        [Required]
        [StringLength(10)]
        public string dis_id { get; set; }

        [Required]
        [StringLength(100)]
        public string dis_name { get; set; }

        [Required]
        public DateTime dis_start { get; set; }

        [Required]
        public DateTime dis_end { get; set; }

        [Range(0, 100)]
        public float dis_percent { get; set; }
    }
}
