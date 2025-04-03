using System.ComponentModel.DataAnnotations;

namespace ADPD_dotNET_Project.Models
{
    public class StudentCourse
    {
        [Key]
        public int StudentId { get; set; }

        [Key]
        public int CourseId { get; set; }

        public User Student { get; set; }
        public Course Course { get; set; }
    }
}
