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

        /*
         [Key]
        [Required]
        public string Id { get; set; }

        public string UserName { get; set; }

        public string NormalizedUserName { get; set; }

        public string Email { get; set; }

        public string NormalizedEmail { get; set; }

        public int EmailConfirmed { get; set; }

        public string PasswordHash { get; set; }

        public string SecurityStamp { get; set; }

        public string ConcurrencyStamp { get; set; }

        public string PhoneNumber { get; set; }

        public int PhoneNumberConfirmed { get; set; }

        public int TwoFactorEnabled { get; set; }

        public DateTime LockoutEnd { get; set; }

        public int LockoutEnabled { get; set; }

        public int AccessFailedCount { get; set; }

        public DateTime cus_dob { get; set; }

        public string cus_gender { get; set; }

        public string cus_name { get; set; }

        public string cus_phone { get; set; }

        public int cus_point { get; set; }

        [Required]
        public string cus_type { get; set; }
         */
    }
}
