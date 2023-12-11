using TodoApp.API.DTOs.Tasks;
using TodoApp.API.Models.Category;

namespace TodoApp.API.Interfaces;

public interface ICategoryRepository
{
    public Task<Guid?> GetIdByTypeAsync(CategoryType type);
    public Task<IEnumerable<CategoryDto>> GetAllForTaskAsync(Guid taskId);
    public Task<bool> CreateCategoryAsync(Category category);
    public Task ClearTableAsync();
}