using TaskMasterAppBLL.Service.Interface;
using TaskMasterAppDAL.Repository.Implement;
using TaskModel = TaskMasterAppDAL.Models.Task;
namespace TaskMasterAppBLL.Service.Implement
{
    public class TaskService : ITaskService
    {

        private Repository<TaskModel> _repository = new Repository<TaskModel>();

        public void AddTask(TaskModel task)
        {
            _repository.Add(task);
        }

        public void DeleteTask(TaskModel task)
        {
            _repository.Delete(task);
        }

        public TaskModel GetTask(int id)
        {
            return _repository.GetById(id);
        }

        public List<TaskModel> GetTasks()
        {
            return _repository.GetAll();
        }

        public void UpdateTask(TaskModel task)
        {
            _repository.Update(task);
        }
    }
}
