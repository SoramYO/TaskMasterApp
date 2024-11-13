using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMasterAppDAL.Models;

namespace TaskMasterAppBLL.Service.Interface
{
    public interface IUserService
    {
        List<User> GetAllUsers();
        void AddUser(User user);
        void UpdateUser(User user);

        User GetByUserName(string username);

        User GetUserById(int userId);
    }
}
