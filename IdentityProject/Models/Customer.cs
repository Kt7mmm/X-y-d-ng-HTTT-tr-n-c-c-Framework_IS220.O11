using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace IdentityProject.Models
{
    [Index(nameof(Customer.cus_phone), IsUnique = true)]
    [Index(nameof(Customer.cus_email), IsUnique = true)]
    public class Customer
    {
        [Key]
        [Required]
        public int c_id { get; set; }

        public string cus_name { get; set; }

        public string cus_phone { get; set; }

        public string cus_gender { get; set; }

        [Required]
        public string cus_email { get; set; }

        public DateTime cus_dob { get; set; }

        [Required]
        public string cus_type { get; set; }

        public int cus_point { get; set; }
    }
}
