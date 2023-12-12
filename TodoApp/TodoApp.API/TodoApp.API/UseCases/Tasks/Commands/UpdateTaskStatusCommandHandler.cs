using MediatR;
using TodoApp.API.Interfaces;

namespace TodoApp.API.UseCases.Tasks.Commands;

internal class UpdateTaskStatusCommandHandler(IApplicationDbContext dbContext, 
    ITasksRepository repo) : IRequestHandler<UpdateTaskStatusCommand, bool>
{
    public async Task<bool> Handle(UpdateTaskStatusCommand request, CancellationToken ct)
    {
        try
        {
            var task = await repo.GetByIdAsync(request.UpdateStatusTaskDto.Id);
            var newStatus = request.UpdateStatusTaskDto.Status;
                
            if (task == null)
            {
                return false;
            }

            if (task.Status != newStatus )
            {
                task.Status = newStatus ;
                return await dbContext.SaveChangesAsyncCheck(ct);
            }
                
            return false; 
        }
            
        catch (Exception ex)
        {
            Console.WriteLine($"Couldn't update status, because: { ex.Message }");
            return false;
        }
    }
}