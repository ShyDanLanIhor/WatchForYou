using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shyryi_WatchForYou.DTOs
{
    [Table("ThingsTable")]
    public class ThingDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        [Required]
        public string Ip { get; set; } = null!;
        [Required]
        public bool IsVideo { get; set; }
        [Required]
        public bool IsAlerted { get; set; }
        public string? Description { get; set; }
        [Required]
        public int AreaId { get; set; }
        [Required]
        public AreaDto Area { get; set; } = null!;
    }
}