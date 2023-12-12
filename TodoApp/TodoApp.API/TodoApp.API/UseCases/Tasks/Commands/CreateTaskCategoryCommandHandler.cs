using MediatR;
using TodoApp.API.Models.TaskCategory;

namespace TodoApp.API.UseCases.Tasks.Commands;

internal class CreateTaskCategoryCommandHandler(ApplicationDbContext dbContext) : IRequestHandler<CreateTaskCategoryCommand, bool>
{
    public async Task<bool> Handle(CreateTaskCategoryCommand request, CancellationToken ct)
    {
        var taskCategory = new TaskCategory { TaskId = request.TaskId, CategoryId = request.CategoryId };
        await dbContext.TaskCategories.AddAsync(taskCategory, ct);
        return await dbContext.SaveChangesAsyncCheck(ct);
    }
}