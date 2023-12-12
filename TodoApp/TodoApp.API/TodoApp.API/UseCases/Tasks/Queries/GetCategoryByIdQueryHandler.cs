using MediatR;
using TodoApp.API.Models.Category;

namespace TodoApp.API.UseCases.Tasks.Queries;

internal class GetCategoryByIdQueryHandler(ApplicationDbContext dbContext) : IRequestHandler<GetCategoryByIdQuery, Category?>
{
    public async Task<Category?> Handle(GetCategoryByIdQuery request, CancellationToken ct)
        => await dbContext.Categories.FindAsync(new object?[] { request.CategoryId, ct }, cancellationToken: ct);
}