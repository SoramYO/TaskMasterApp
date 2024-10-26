using TaskMasterAppBLL.Service.Interface;
using TaskMasterAppDAL.Models;
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

        public List<TaskModel> GetTaskByDay(DateTime date)
        {
            return _repository.GetAll().Where(x => x.CreatedDate?.Date == date.Date).ToList();
        }

        public List<TaskModel> GetTasks()
        {
            return _repository.GetAll();
        }

        public void UpdateTask(TaskModel task)
        {
            _repository.Update(task);
        }
        public void CheckTask(TaskModel task)
        {
            task.IsCompleted = !task.IsCompleted;
            _repository.Update(task);
        }
        public void MuteTask(TaskModel task)
        {
            task.Notification = !task.Notification;
            _repository.Update(task);
        }

        public List<TaskModel> TaskByCategory(Category category)
        {
            return GetTasks().Where(t => t.CategoryId == category.CategoryId).ToList();
        }

        public List<TaskModel> GetTasksByUserId(int userId)
        {
            return GetTasks().Where(t => t.UserId == userId).ToList();
        }
    }
}
