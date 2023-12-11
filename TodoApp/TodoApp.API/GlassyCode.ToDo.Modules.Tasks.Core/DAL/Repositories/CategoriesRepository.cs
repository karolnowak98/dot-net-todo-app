using GlassyCode.ToDo.Modules.Tasks.Core.DTOs;
using GlassyCode.ToDo.Modules.Tasks.Core.Entities;
using GlassyCode.ToDo.Modules.Tasks.Core.Entities.Enums;
using Task = System.Threading.Tasks.Task;

namespace GlassyCode.ToDo.Modules.Tasks.Core.DAL.Repositories;

public class CategoriesRepository : ICategoryRepository
{
    public CategoriesRepository(ApplicationDbContext context)
    {
            _context = context;
        }
        
    private readonly ApplicationDbContext _context;

    private async Task<Category?> GetCategoryByIdAsync(Guid categoryId) => await _context.Categories.FindAsync(categoryId);
    private async Task<Category?> GetCategoryByTypeAsync(CategoryType type) => await _context.Categories.FirstOrDefaultAsync(c => c!.Type == type);
    private async Task<IEnumerable<TaskCategory>> GetTaskCategoriesForTaskAsync(Guid taskId) => await _context.TaskCategories.Where(tc => tc.TaskId == taskId).ToListAsync();
        
    public async Task<Guid?> GetIdByTypeAsync(CategoryType type)
    {
            var category = await GetCategoryByTypeAsync(type);
            
            return category?.Id;
        }

    public async Task<bool> CreateCategoryAsync(Category category)
    {
            await _context.Categories.AddAsync(category);
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

    public async Task ClearTableAsync()
    {
            var categories = await _context.Categories.ToListAsync();
            _context.Categories.RemoveRange(categories);
            await _context.SaveChangesAsync();
        }
        

    public async Task<IEnumerable<CategoryDto>> GetAllForTaskAsync(Guid taskId)
    {
            var taskCategories = await GetTaskCategoriesForTaskAsync(taskId);
            var categories = new List<CategoryDto>();
            
            foreach (var taskCategory in taskCategories)
            {
                var category = await GetCategoryByIdAsync(taskCategory.CategoryId);

                if (category == null)
                {
                    continue;
                }
                
                categories.Add(new CategoryDto { Type = category.Type });
            }

            return categories;
        }
}