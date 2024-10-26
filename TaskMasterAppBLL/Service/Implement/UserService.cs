using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMasterAppBLL.Service.Interface;
using TaskMasterAppDAL.Models;
using TaskMasterAppDAL.Repository.Implement;

namespace TaskMasterAppBLL.Service.Implement
{
    public class UserService : IUserService
    {
        private Repository<User> _repository = new Repository<User>();
        private ITaskService _taskService = new TaskService();
        public List<User> GetAllUsers()
        {
            return _repository.GetAll();
        }

    }
}
