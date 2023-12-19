using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IdentityProject.Models
{
    public class Ticket
    {
        [Key]
        [Required]
        [StringLength(10)]
        public string tk_id { get; set; }

        [Required]
        [StringLength(10)]
        public string mv_id { get; set; }

        [Key]
        [Required]
        [StringLength(10)]
        public string sl_id { get; set; }

        [Required]
        [StringLength(10)]
        public string r_id { get; set; }

        [Required]
        [Range(0,Double.MaxValue)]
        public Decimal tk_value { get; set; }

        [Key]
        [Required]
        [StringLength(10)]
        public string st_id { get; set; }

        [Required]
        [StringLength(20)]
        public string tk_type { get; set; }

        [Required]
        [StringLength(10)]
        public string bi_id { get; set; }

        [Required]
        public int tk_available { get; set; }

        [ForeignKey("sl_id, r_id, mv_id")]
        public virtual Slot slot { get; set; }

        [ForeignKey("st_id, r_id")]
        public virtual Seat seat { get; set; }

        [ForeignKey("bi_id")]
        public virtual Bill bill { get; set; }

    }
}
