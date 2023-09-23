using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Shyryi_WatchForYou.DTOs
{
    [Table("AreasTable")]
    public class AreaDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        [Required]
        public int UserId { get; set; }
        [Required]
        public UserDto User { get; set; } = null!;
        public ICollection<ThingDto>? Things { get; set; }
    }
}