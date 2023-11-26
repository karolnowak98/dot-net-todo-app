using TodoApp.API.Models.Category;
using TodoApp.API.Models.Category.Enums;

namespace TodoApp.API.Repositories.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<Guid?> GetIdByTypeAsync(CategoryType type);
        public Task<IEnumerable<CategoryDto>> GetAllForTaskAsync(Guid taskId);
        public Task<bool> CreateCategoryAsync(Category category);
        public Task ClearTableAsync();
    }
}