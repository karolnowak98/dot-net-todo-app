using GlassyCode.ToDo.Modules.Tasks.Core.DTOs;
using GlassyCode.ToDo.Modules.Tasks.Core.Entities;
using GlassyCode.ToDo.Modules.Tasks.Core.Entities.Enums;
using Task = System.Threading.Tasks.Task;

namespace GlassyCode.ToDo.Modules.Tasks.Core.Repositories;

public interface ICategoryRepository
{
    public Task<Guid?> GetIdByTypeAsync(CategoryType type);
    public Task<IEnumerable<CategoryDto>> GetAllForTaskAsync(Guid taskId);
    public Task<bool> CreateCategoryAsync(Category category);
    public Task ClearTableAsync();
}