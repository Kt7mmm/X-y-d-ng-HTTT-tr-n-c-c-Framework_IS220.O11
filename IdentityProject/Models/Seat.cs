using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace IdentityProject.Models
{

    public class Seat
    {
        [Key]
        [Required]
        [StringLength(10)]
        public string st_id { get; set; }

        [Key]
        [Required]
        [StringLength(10)]
        public string r_id { get; set; }
        [ForeignKey(nameof(r_id))]
        public virtual Room room { get; set; }

        [Required]
        [StringLength(20)]
        public string st_type { get; set; }
    }
}
