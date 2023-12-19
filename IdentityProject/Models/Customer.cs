using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace cinema.Models
{
    [Index(nameof(Customer.cus_phone), IsUnique = true)]
    [Index(nameof(Customer.cus_email), IsUnique = true)]
    public class Customer
    {
        [Key]
        [Required]
        [StringLength(10)]
        public string cus_id { get; set; }

        [StringLength(100)]
        public string cus_name { get; set; }

        [StringLength(10)]
        public string cus_phone { get; set; }

        [StringLength(10)]
        public string cus_gender { get; set; }

        [Required]
        [StringLength(50)]
        public string cus_email { get; set; }

        public DateTime cus_dob { get; set; }

        [Required]
        [StringLength(30)]
        public string cus_type { get; set; }

        [StringLength(100)]
        public int cus_point { get; set; }
    }
}
