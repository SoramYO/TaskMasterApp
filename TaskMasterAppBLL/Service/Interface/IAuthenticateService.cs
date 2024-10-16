using TaskMasterAppDAL.Models;

namespace TaskMasterAppBLL.Service.Interface
{
    public interface IAuthenticateService
    {
        User AuthenticateUser(string username, string password);
        int GetEmployeeId(int userId);

    }
}
