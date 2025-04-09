using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ADPD_dotNET_Project.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Required, MaxLength(100)]
        public string Username { get; set; }

        [Required, MaxLength(255)] 
        public string Password { get; set; }

        [Required, MaxLength(255)]
        public string FullName { get; set; }

        [Required, EmailAddress]
        public string Email { get; set; }
        
        public int? RoleId { get; set; }

        [ForeignKey("RoleId")]
        public Role Role { get; set; }
    }
}  