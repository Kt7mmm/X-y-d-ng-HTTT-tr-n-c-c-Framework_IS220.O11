using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace IdentityProject.Models
{
    public class ChooseType
    {
        [Key]
        [Required]
        [StringLength(50)]
        public string type_id { get; set; }
        [ForeignKey(nameof(type_id))]
        public virtual MovieType movietype { get; set; }

        [Key]
        [Required]
        [StringLength(50)]
        public string mv_id { get; set; }
        [ForeignKey(nameof(mv_id))]
        public virtual Movie movie { get; set; }
    }
}
