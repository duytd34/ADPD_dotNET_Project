using ADPD_dotNET_Project.Models;
using ADPD_dotNET_Project.Repositories;


namespace ADPD_dotNET_Project.Facade
{
    public class CourseFacade : ICourseFacade
    {
        private readonly ICourseRepository _courseRepository;

        public CourseFacade(ICourseRepository courseRepository)
        {
            _courseRepository = courseRepository;
        }

        public IEnumerable<Course> GetAllCourses() => _courseRepository.GetAll();

        public Course GetCourseById(int id) => _courseRepository.GetById(id);

        public void AddCourse(Course course) => _courseRepository.Add(course);

        public void UpdateCourse(Course course) => _courseRepository.Update(course);

        public void DeleteCourse(int id) => _courseRepository.Delete(id);
    }
}
