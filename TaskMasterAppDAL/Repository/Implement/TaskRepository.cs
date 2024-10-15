using TaskMasterAppDAL.Repository.Interface;
using TaskModel = TaskMasterAppDAL.Models.Task;
namespace TaskMasterAppDAL.Repository.Implement
{

    public class TaskRepository : Repository<TaskModel>, ITaskRepository
    {

    }
}
