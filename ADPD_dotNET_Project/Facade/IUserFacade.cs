
using ADPD_dotNET_Project.Models;

namespace ADPD_dotNET_Project.Facade
{
    public interface IUserFacade
    {
        bool RegisterUser(string username, string password, string fullName, string email);
        User AuthenticateUser(string username, string password);
        void RegisterUser(User user);
    }
}
