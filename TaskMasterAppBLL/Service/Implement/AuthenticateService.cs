using TaskMasterAppBLL.Service.Interface;
using TaskMasterAppDAL.Models;
using TaskMasterAppDAL.Repository.Implement;
using TaskMasterAppDAL.Repository.Interface;

namespace TaskMasterAppBLL.Service.Implement
{
    public class AuthenticateService : IAuthenticateService
    {
        private IAuthenticateRepository _authenticateRepository;

        private Repository<User> _userRepository = new Repository<User>();

        public AuthenticateService()
        {
            _authenticateRepository = new AuthenticateRepository();
        }
        public User AuthenticateUser(string value, string password)
        {
            var user = _authenticateRepository.AuthenticateUser(value, password);
            return user;
        }

        public int GetEmployeeId(int userId)
        {
            var user = _userRepository.GetById(userId);
            return user.UserId;
        }
    }
}
