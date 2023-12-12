using TodoApp.API.DTOs.Tasks;
using TodoApp.API.Exceptions;
using TodoApp.API.Interfaces;
using TodoApp.API.Models.Category;
using TodoApp.API.Models.TaskCategory;

namespace TodoApp.API.Core.Data.Repositories;

public class CategoriesRepository(ApplicationDbContext context) : ICategoryRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new DbContextNullException(nameof(context));
        
    private async Task<Category?> GetCategoryByIdAsync(Guid categoryId) 
        => await _context.Categories.FindAsync(categoryId);
    private async Task<Category?> GetCategoryByTypeAsync(CategoryType type) 
        => await _context.Categories.FirstOrDefaultAsync(c => c!.Type == type);
    private async Task<IEnumerable<TaskCategory>> GetTaskCategoriesForTaskAsync(Guid taskId) 
        => await _context.TaskCategories.Where(tc => tc.TaskId == taskId).ToListAsync();
        
    public async Task<Guid?> GetIdByTypeAsync(CategoryType type)
    {
        var category = await GetCategoryByTypeAsync(type);
            
        return category?.Id;
    }

    public async Task<bool> CreateCategoryAsync(Category category)
    {
        await context.Categories.AddAsync(category);
        var changes = await context.SaveChangesAsync();
        return changes > 0;
    }

    public async Task ClearTableAsync()
    {
        var categories = await context.Categories.ToListAsync();
        context.Categories.RemoveRange(categories);
        await context.SaveChangesAsync();
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