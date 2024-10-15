using TaskModel = TaskMasterAppDAL.Models.Task;
namespace TaskMasterAppBLL.Service.Interface
{
    public interface ITaskService
    {
        List<TaskModel> GetTasks();
        TaskModel GetTask(int id);

        void AddTask(TaskModel task);

        void UpdateTask(TaskModel task);

        void DeleteTask(TaskModel task);
    }
}
