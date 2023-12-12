using MediatR;
using TodoApp.API.Models.TaskCategory;

namespace TodoApp.API.UseCases.Tasks.Queries;

internal class GetTaskCategoriesForTaskQueryHandler(ApplicationDbContext dbContext)
    : IRequestHandler<GetTaskCategoriesForTaskQuery, IEnumerable<TaskCategory>>
{
    public async Task<IEnumerable<TaskCategory>> Handle(GetTaskCategoriesForTaskQuery request, CancellationToken ct)
        => await dbContext.TaskCategories.Where(tc => tc.TaskId == request.TaskId).ToListAsync(cancellationToken: ct);
}