using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace cinema.Models
{
    [PrimaryKey(nameof(sl_id), nameof(r_id),nameof(mv_id))]
    public class Slot
    {
        [Required]
        [StringLength(10)]
        public string sl_id { get; set; }

        [Required]
        [StringLength(10)]
        public string r_id { get; set; }
        [ForeignKey(nameof(r_id))]
        public virtual Room room { get; set; }

        [Required]
        [StringLength(10)]
        public string mv_id { get; set; }
        [ForeignKey(nameof(mv_id))]
        public virtual Movie movie { get; set; }

        [Required]
        public TimeSpan sl_duration { get; set; }

        [Required]
        public DateTime sl_start { get; set; }

        public DateTime sl_end { get; set; }

        [Required]
        public Decimal sl_price { get; set; }
    }
}
