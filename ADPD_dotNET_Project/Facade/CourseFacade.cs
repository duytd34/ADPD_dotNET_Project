using ADPD_dotNET_Project.Models;
using ADPD_dotNET_Project.Repositories;

namespace ADPD_dotNET_Project.Services.Facade
{
    public class CourseFacade
    {
        private readonly ICourseRepository _courseRepo;

        public CourseFacade(ICourseRepository courseRepo)
        {
            _courseRepo = courseRepo;
        }

        public void AddCourse(Course course)
        {
            _courseRepo.Add(course);
        }

        public void UpdateCourse(Course course)
        {
            _courseRepo.Update(course);
        }

        public void DeleteCourse(int id)
        {
            _courseRepo.Delete(id);
        }
    }
}
