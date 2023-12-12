using MediatR;
using TodoApp.API.Interfaces;
using TodoApp.API.Models.Category;

namespace TodoApp.API.UseCases.Tasks.Queries;

internal class GetCategoryByTypeQueryHandler(IApplicationDbContext dbContext) : IRequestHandler<GetCategoryByTypeQuery, Category?>
{
    public async Task<Category?> Handle(GetCategoryByTypeQuery request, CancellationToken ct)
        => await dbContext.Categories.Where(c => c.Type == request.Type).FirstOrDefaultAsync(cancellationToken: ct);
}