using TaskMasterAppBLL.Service.Interface;
using TaskMasterAppDAL.Models;
using TaskMasterAppDAL.Repository.Implement;
using TaskMasterAppDAL.Repository.Interface;

namespace TaskMasterAppBLL.Service.Implement
{
    public class AuthenticateService : IAuthenticateService
    {
        private IAuthenticateRepository _authenticateRepository;

        public AuthenticateService()
        {
            _authenticateRepository = new AuthenticateRepository();
        }
        public User AuthenticateUser(string value, string password)
        {
            var user = _authenticateRepository.AuthenticateUser(value, password);
            return user;
        }


    }
}
