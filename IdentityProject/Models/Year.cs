using System.ComponentModel.DataAnnotations;

namespace cinema.Models
{
    public class Year
    {
        [Key]
        [Required]
        [StringLength(10)]
        public string yre_id { get; set; }

        [Required]
        public int yre_year { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public int yre_count { get; set; }

        [Required]
        [Range (0, Double.MaxValue)]
        public decimal yre_value { get; set; }
    }
}
