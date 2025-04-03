using System.ComponentModel.DataAnnotations;

namespace ADPD_dotNET_Project.Models
{
    public class FacultyCourse
    {
        [Key]
        public int FacultyId { get; set; }

        [Key]
        public int CourseId { get; set; }

        public User Faculty { get; set; }
        public Course Course { get; set; }
    }
}
