using TaskMasterAppDAL.Models;

namespace TaskMasterAppDAL.Repository.Interface
{
    public interface IAuthenticateRepository
    {
        User AuthenticateUser(string username, string password);
    }
}
