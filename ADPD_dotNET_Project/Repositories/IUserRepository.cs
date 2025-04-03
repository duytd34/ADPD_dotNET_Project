using ADPD_dotNET_Project.Models;

namespace ADPD_dotNET_Project.Repositories
{
    public interface IUserRepository
    {
        void Add(User user);
        User GetByUsername(string username);
        bool Exists(string username);
    }
}
