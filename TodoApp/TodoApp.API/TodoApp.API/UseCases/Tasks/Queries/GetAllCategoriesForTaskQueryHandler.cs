using MediatR;
using TodoApp.API.DTOs.Tasks;
using TodoApp.API.Interfaces;

namespace TodoApp.API.UseCases.Tasks.Queries;

internal class GetAllCategoriesForTaskQueryHandler(ICategoryRepository repo) 
    : IRequestHandler<GetAllCategoriesForTaskQuery, IEnumerable<CategoryDto>>
{
    public async Task<IEnumerable<CategoryDto>> Handle(GetAllCategoriesForTaskQuery request, CancellationToken cancellationToken)
    {
        var taskCategories = await repo.GetTaskCategoriesForTaskAsync(request.TaskId);
        var categories = new List<CategoryDto>();
        
        foreach (var taskCategory in taskCategories)
        {
            var category = await repo.GetCategoryByIdAsync(taskCategory.CategoryId);

            if (category == null)
            {
                continue;
            }
                
            categories.Add(new CategoryDto { Type = category.Type });
        }

        return categories;
    }
}