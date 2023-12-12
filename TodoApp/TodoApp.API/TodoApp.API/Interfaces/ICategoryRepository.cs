using TodoApp.API.DTOs.Tasks;
using TodoApp.API.Models.Category;
using TodoApp.API.Models.TaskCategory;

namespace TodoApp.API.Interfaces;

public interface ICategoryRepository
{
    Task<Category?> GetCategoryByIdAsync(Guid categoryId);
    Task<Category?> GetCategoryByTypeAsync(CategoryType type);
    Task<IEnumerable<TaskCategory>> GetTaskCategoriesForTaskAsync(Guid taskId);
    Task<Guid?> GetIdByTypeAsync(CategoryType type);
    Task<IEnumerable<CategoryDto>> GetAllForTaskAsync(Guid taskId);
    Task<bool> CreateCategoryAsync(Category category);
    Task<bool>  ClearTableAsync();
}