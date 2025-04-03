using ADPD_dotNET_Project.Data;
using ADPD_dotNET_Project.Models;

namespace ADPD_dotNET_Project.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _context;

        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public void Add(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public User GetByUsername(string username)
        {
            return _context.Users.FirstOrDefault(u => u.Username == username);
        }

        public bool Exists(string username)
        {
            return _context.Users.Any(u => u.Username == username);
        }


    }
}

