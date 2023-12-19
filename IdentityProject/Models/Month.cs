using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace IdentityProject.Models
{

    public class Month
    {
        [Key]
        [Required]
        [StringLength(10)]
        public string mre_id { get; set; }

        [Required]
        public int mre_month { get; set; }

        [Key]
        [Required]
        [StringLength(10)]
        public string mre_yre_id { get; set; }
        [ForeignKey(nameof(mre_yre_id))]
        public virtual Year year { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int mre_count { get; set; }

        [Required]
        [Range(0, Double.MaxValue)]
        public decimal mre_value { get; set; }
    }
}
