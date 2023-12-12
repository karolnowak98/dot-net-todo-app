using MediatR;
using TodoApp.API.Core.Data.Identity;

namespace TodoApp.API.UseCases.Users.Queries;

internal class GetRolesQueryHandler(UserManager<ApplicationUser> userManager) : IRequestHandler<GetRolesQuery, IEnumerable<string>>
{
    public async Task<IEnumerable<string>> Handle(GetRolesQuery request, CancellationToken ct)
        => await userManager.GetRolesAsync(request.User);
}