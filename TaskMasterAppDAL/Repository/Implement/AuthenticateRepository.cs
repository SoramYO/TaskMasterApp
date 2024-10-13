using Microsoft.EntityFrameworkCore;
using TaskMasterAppDAL.Models;
using TaskMasterAppDAL.Repository.Interface;

namespace TaskMasterAppDAL.Repository.Implement
{
    public class AuthenticateRepository : IAuthenticateRepository
    {
        private TaskMasterContext _context;


        public User AuthenticateUser(string value, string password)
        {
            _context = new TaskMasterContext();
            var user = _context.Users.Include(u => u.Roles)
                .FirstOrDefault(u => (u.Username == value || u.Email == value) && u.PasswordHash == password);

            return user;
        }
    }
}
