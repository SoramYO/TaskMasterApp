using TaskMasterAppDAL.Models;

namespace TaskMasterAppBLL.Service.Interface
{
    public interface ITaskCategoryService
    {
        List<Category> GetCategory();
        void AddCategory(Category category);
        void UpdateCategory(Category category);
        void DeleteCategory(Category category);
    }
}
