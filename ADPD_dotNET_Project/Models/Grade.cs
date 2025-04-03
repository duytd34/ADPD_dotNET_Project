using System.ComponentModel.DataAnnotations;

namespace ADPD_dotNET_Project.Models
{
    public class Grade
    {
        [Key]
        public int StudentId { get; set; }

        [Key]
        public int CourseId { get; set; }

        [Required]
        public int GradeValue { get; set; }

        public int? GradedBy { get; set; }
        public User GradedByUser { get; set; }

        [Required]
        public DateTime GradedDate { get; set; }

        [MaxLength(500)]
        public string Comments { get; set; }

        [Required, MaxLength(50)]
        public string Status { get; set; } // Pending, Approved, Rejected
    }

}
