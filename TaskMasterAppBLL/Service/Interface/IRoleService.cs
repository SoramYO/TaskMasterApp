using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskMasterAppDAL.Models;

namespace TaskMasterAppBLL.Service.Interface
{
    public interface IRoleService
    {
        List<Role> GetAllRole();
    }
}
