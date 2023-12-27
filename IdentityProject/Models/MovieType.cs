using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace IdentityProject.Models
{
    [Index(nameof(MovieType.type_name), IsUnique = true)]
    public class MovieType
    {
        [Key]
        [Required]
        [StringLength(50)]
        public string type_id { get; set; }

        [Required]
        [StringLength(100)]
        public string type_name { get; set; }
    }
}
