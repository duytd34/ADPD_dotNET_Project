using ADPD_dotNET_Project.Models;

namespace ADPD_dotNET_Project.Facade
{
    public interface ICourseFacade
    {
        IEnumerable<Course> GetAllCourses();
        Course GetCourseById(int id);
        void AddCourse(Course course);
        void UpdateCourse(Course course);
        void DeleteCourse(int id);
    }
}
