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

        public void AddUser(User user)
        {
            _repository.Add(user);
        }

        public void UpdateUser(User user)
        {
            var existingUser = GetUserById(user.UserId);
            existingUser.UserName = user.UserName;
            existingUser.Email = user.Email;
            existingUser.PasswordHash = user.PasswordHash;
            existingUser.RoleId = user.RoleId;
            _repository.Update(existingUser);
        }

        public User GetByUserName(string username)
        {
            return _repository.GetAll().FirstOrDefault(x => x.UserName == username);
        }

        public User GetUserById(int userId)
        {
            return _repository.GetById(userId);
        }

    }
}
