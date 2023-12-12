using MediatR;
using Task = TodoApp.API.Models.Task.Task;

namespace TodoApp.API.UseCases.Tasks.Queries;

internal class GetAllTasksForUserQueryHandler(ApplicationDbContext dbContext)
    : IRequestHandler<GetAllTasksForUserQuery, IEnumerable<Task>>
{
    
    public async Task<IEnumerable<Task>> Handle(GetAllTasksForUserQuery request, CancellationToken ct)
        => await dbContext.Tasks.Where(c => c.UserId == request.UserId).ToListAsync(cancellationToken: ct);
}