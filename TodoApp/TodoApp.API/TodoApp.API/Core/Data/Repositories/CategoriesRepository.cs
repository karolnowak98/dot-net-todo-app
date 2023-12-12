using MediatR;
using TodoApp.API.DTOs.Tasks;
using TodoApp.API.Interfaces;
using TodoApp.API.Models.Category;
using TodoApp.API.Models.TaskCategory;
using TodoApp.API.UseCases.Tasks.Commands;
using TodoApp.API.UseCases.Tasks.Queries;

namespace TodoApp.API.Core.Data.Repositories;

public class CategoriesRepository(ISender sender) : ICategoryRepository
{
    public async Task<Category?> GetCategoryByIdAsync(Guid categoryId)
        => await sender.Send(new GetCategoryByIdQuery(categoryId));

    public async Task<Category?> GetCategoryByTypeAsync(CategoryType type)
        => await sender.Send(new GetCategoryByTypeQuery(type));
    
    public async Task<IEnumerable<TaskCategory>> GetTaskCategoriesForTaskAsync(Guid taskId) 
        => await sender.Send(new GetTaskCategoriesForTaskQuery(taskId));

    public async Task<Guid?> GetIdByTypeAsync(CategoryType type)
        => await sender.Send(new GetCategoryIdByTypeQuery(type));

    public async Task<bool> CreateCategoryAsync(Category category)
        => await sender.Send(new CreateCategoryCommand(category));

    public async Task<bool> ClearTableAsync()
        => await sender.Send(new ClearTableCommand());

    public async Task<IEnumerable<CategoryDto>> GetAllForTaskAsync(Guid taskId)
        => await sender.Send(new GetAllCategoriesForTaskQuery(taskId));
}