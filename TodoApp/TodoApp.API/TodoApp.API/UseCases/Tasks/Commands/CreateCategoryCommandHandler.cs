using MediatR;

namespace TodoApp.API.UseCases.Tasks.Commands;

internal class CreateCategoryCommandHandler(ApplicationDbContext dbContext) : IRequestHandler<CreateCategoryCommand, bool>
{
    public async Task<bool> Handle(CreateCategoryCommand request, CancellationToken ct)
    {
        await dbContext.Categories.AddAsync(request.Category, ct);
        return await dbContext.SaveChangesAsyncCheck(ct);
    }
}