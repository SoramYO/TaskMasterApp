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
    public class RoleService : IRoleService
    {
        private Repository<Role> _roleRepository = new Repository<Role>();
        public List<Role> GetAllRole()
        {
            return _roleRepository.GetAll();
        }
    }
}
