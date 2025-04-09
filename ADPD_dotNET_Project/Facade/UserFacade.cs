using ADPD_dotNET_Project.Models;
using ADPD_dotNET_Project.Repositories;
using BCrypt.Net;
using ADPD_dotNET_Project.Repositories;
namespace ADPD_dotNET_Project.Facade
{
    public class UserFacade : IUserFacade
    {
        private readonly IUserRepository _userRepository;

        public UserFacade(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public bool RegisterUser(string username, string password, string fullName, string email)
        {
            if (_userRepository.Exists(username))
                return false;

            var user = new User
            {
                Username = username,
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                FullName = fullName,
                Email = email,
                RoleId = 3
            };

            _userRepository.Add(user);
            return true;
        }


        public User AuthenticateUser(string username, string password)
        {
            var user = _userRepository.GetByUsername(username);
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return user;  
            }
            return null; 
        }

        public void RegisterUser(User user)
        {
            // Mặc định RoleId là 3 (Student)
            user.RoleId = 3;

           
            user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);

            _userRepository.Add(user);
        }

    }
}
