using ADPD_dotNET_Project.Models;

namespace ADPD_dotNET_Project.Repositories
{
    public interface IEnrollmentRepository
    {
        IEnumerable<Enrollment> GetAll();
        void Add(Enrollment enrollment);
        void Delete(int enrollmentId);
        IEnumerable<Enrollment> GetByStudentId(int studentId);
    }

}
