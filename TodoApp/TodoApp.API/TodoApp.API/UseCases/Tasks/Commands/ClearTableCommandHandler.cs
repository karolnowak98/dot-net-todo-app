using MediatR;
using TodoApp.API.Interfaces;

namespace TodoApp.API.UseCases.Tasks.Commands;

internal class ClearTableCommandHandler(IApplicationDbContext dbContext)
    : IRequestHandler<ClearTableCommand, bool>
{
    public async Task<bool> Handle(ClearTableCommand request, CancellationToken ct)
    {
        var categories = await dbContext.Categories.ToListAsync(cancellationToken: ct);
        dbContext.Categories.RemoveRange(categories);
        return await dbContext.SaveChangesAsyncCheck(ct);
    }
}