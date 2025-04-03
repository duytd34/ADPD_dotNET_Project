using ADPD_dotNET_Project.Models;

namespace ADPD_dotNET_Project.Repositories
{
    public interface ICourseRepository
    {
        IEnumerable<Course> GetAll();
        Course GetById(int id);
        void Add(Course course);
        void Update(Course course);
        void Delete(int id);
        void Save();
    }
}
