using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace cinema.Models
{
    [PrimaryKey(nameof(type_id), nameof(mv_id))]
    public class ChooseType
    {
        [Required]
        [StringLength(50)]
        public string type_id { get; set; }
        [ForeignKey(nameof(type_id))]
        public virtual MovieType movietype { get; set; }

        [Required]
        [StringLength(50)]
        public string mv_id { get; set; }
        [ForeignKey(nameof(mv_id))]
        public virtual Movie movie { get; set; }
    }
}
