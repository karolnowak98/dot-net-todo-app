using MediatR;
using TodoApp.API.Interfaces;

namespace TodoApp.API.UseCases.Tasks.Queries;

internal class GetCategoryIdByTypeQueryHandler(ICategoryRepository repo) : IRequestHandler<GetCategoryIdByTypeQuery, Guid?>
{
    public async Task<Guid?> Handle(GetCategoryIdByTypeQuery request, CancellationToken ct)
    {
        var category = await repo.GetCategoryByTypeAsync(request.Type);
        return category?.Id;
    } 
}