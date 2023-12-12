using MediatR;

namespace TodoApp.API.UseCases.Tasks.Commands;

internal class CreateTaskCommandHandler(ApplicationDbContext dbContext) : IRequestHandler<CreateTaskCommand, bool>
{
    public async Task<bool> Handle(CreateTaskCommand request, CancellationToken cancellationToken)
    {
        dbContext.Tasks.Add(request.Task);
        return await dbContext.SaveChangesAsyncCheck(cancellationToken);
    }
}