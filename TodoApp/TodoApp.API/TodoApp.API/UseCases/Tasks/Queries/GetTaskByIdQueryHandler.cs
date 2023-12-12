using MediatR;
using Task = TodoApp.API.Models.Task.Task;

namespace TodoApp.API.UseCases.Tasks.Queries;

internal class GetTaskByIdQueryHandler(ApplicationDbContext dbContext)
    : IRequestHandler<GetTaskByIdQuery, Task?>
{
    public async Task<Task?> Handle(GetTaskByIdQuery request, CancellationToken ct)
        => await dbContext.Tasks.Where(c => c.UserId == request.TaskId).FirstOrDefaultAsync(cancellationToken: ct);
}