using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace cinema.Models
{
    [PrimaryKey(nameof(st_id), nameof(r_id))]
    public class Seat
    {
        [Required]
        [StringLength(10)]
        public string st_id { get; set; }

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
