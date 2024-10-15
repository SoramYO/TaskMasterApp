using TaskMasterAppBLL.Service.Interface;
using TaskMasterAppDAL.Models;
using TaskMasterAppDAL.Repository.Implement;
using TaskMasterAppDAL.Repository.Interface;

namespace TaskMasterAppBLL.Service.Implement
{
    public class TaskCategoryService : ITaskCategoryService
    {
        private ITaskCategoryRepository _taskCategoryRepository;
        private Repository<Category> _repository = new Repository<Category>();
        public void AddCategory(Category category)
        {
            _repository.Add(new Category());
        }

        public void DeleteCategory(Category category)
        {
            _repository.Delete(category);
        }

        public List<Category> GetCategory()
        {
            return _repository.GetAll();
        }

        public void UpdateCategory(Category category)
        {
            _repository.Update(category);
        }
    }
}
