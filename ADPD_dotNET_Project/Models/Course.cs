using System.ComponentModel.DataAnnotations;

namespace ADPD_dotNET_Project.Models
{
    public class Course
    {
        [Key]
        public int CourseId { get; set; }

        [Required, MaxLength(255)]
        public string CourseName { get; set; }

        [MaxLength(500)]
        public string Description { get; set; }

        [Required]
        public int CreditHours { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }
    }
}
