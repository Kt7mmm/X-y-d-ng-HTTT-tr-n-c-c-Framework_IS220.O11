using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace IdentityProject.Models
{
    public class Movie
    {
        [Key]
        [Required]
        [StringLength(50)]
        public string mv_id { get; set; }

        [Required]
        [StringLength(100)]
        public string mv_name { get; set; }

        [Required]
        public DateTime mv_start { get; set; }

        [Required]
        public DateTime mv_end { get; set; }

        [Required]
        public TimeSpan mv_duration { get; set; }

        [Required]
        [StringLength(10)]
        public string mv_restrict { get; set; }

        [Required]
        [StringLength(20)]
        public string mv_cap { get; set; }

        [Required]
        [StringLength(500)]
        public string mv_link_poster { get; set; }

        [Required]
        [StringLength(500)]
        public string mv_link_trailer { get; set; }

        [StringLength(7000)]
        public string mv_detail { get; set; }
    }
}
