using System.ComponentModel.DataAnnotations;

namespace ADPD_dotNET_Project.Models
{
    public class Role
    {
        [Key]
        public int RoleId { get; set; }

        [Required, MaxLength(50)]
        public string RoleName { get; set; } 
    }
}
