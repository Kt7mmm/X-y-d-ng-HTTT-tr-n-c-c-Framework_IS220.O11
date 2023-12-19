using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace cinema.Models
{
    public class Bill
    {
        [Key]
        [Required]
        [StringLength(10)]
        public string bi_id { get; set; }


        [Required]
        [StringLength(10)]
        public string cus_id { get; set; }
        [ForeignKey(nameof(cus_id))]
        public virtual Customer Customer { get; set; }

        [Required]
        public DateTime bi_date { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int tk_count { get; set; }

        [Required]
        [Range(0, (double)Decimal.MaxValue)]
        public Decimal bi_value { get; set; }
    }
}
