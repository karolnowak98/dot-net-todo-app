using MediatR;
using TodoApp.API.Core.Data.Identity;

namespace TodoApp.API.UseCases.Users.Queries;

internal class GetByEmailAsyncQueryHandler(ApplicationDbContext context) : IRequestHandler<GetByEmailQuery, ApplicationUser?>
{
    public async Task<ApplicationUser?> Handle(GetByEmailQuery request, CancellationToken ct)
        => await context.Users.Where(u => u.Email == request.Email).FirstOrDefaultAsync(cancellationToken: ct);

}