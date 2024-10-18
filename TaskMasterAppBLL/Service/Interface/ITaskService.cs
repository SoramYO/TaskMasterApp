using TaskModel = TaskMasterAppDAL.Models.Task;
namespace TaskMasterAppBLL.Service.Interface
{
    public interface ITaskService
    {
        List<TaskModel> GetTasks();
        TaskModel GetTask(int id);

        List<TaskModel> GetTaskByDay(DateTime date);

        void AddTask(TaskModel task);

        void UpdateTask(TaskModel task);

        void DeleteTask(TaskModel task);
        void CheckTask(TaskModel task);
        void MuteTask(TaskModel task);
    }
}
