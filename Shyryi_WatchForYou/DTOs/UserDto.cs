using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shyryi_WatchForYou.DTOs
{
    [Table("UsersTable")]
    public class UserDto
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Username { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        [Required]
        public string Email { get; set; } = null!;
        public bool IsVerificated { get; set; }
        public string? ConfirmationToken { get; set; }
        public ICollection<AreaDto>? Areas { get; set; }
    }
}
