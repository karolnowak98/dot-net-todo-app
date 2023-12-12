using MediatR;
using TodoApp.API.Core.Data.Identity;

namespace TodoApp.API.UseCases.Users.Queries;

internal class CheckPasswordQueryHandler(UserManager<ApplicationUser> userManager) : IRequestHandler<CheckPasswordQuery, bool>
{
    public async Task<bool> Handle(CheckPasswordQuery request, CancellationToken ct)
        => await userManager.CheckPasswordAsync(request.User, request.Password);
}