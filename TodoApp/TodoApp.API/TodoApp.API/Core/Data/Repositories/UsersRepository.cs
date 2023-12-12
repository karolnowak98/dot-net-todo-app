using MediatR;
using TodoApp.API.Core.Data.Identity;
using TodoApp.API.DTOs;
using TodoApp.API.DTOs.Users;
using TodoApp.API.Interfaces;
using TodoApp.API.UseCases.Users.Commands;
using TodoApp.API.UseCases.Users.Queries;

namespace TodoApp.API.Core.Data.Repositories;

public class UsersRepository(IMediator mediator) : IUsersRepository
{
    public async Task<ApplicationUser?> GetByEmailAsync(string email)
        => await mediator.Send(new GetByEmailQuery(email));

    public async Task<IEnumerable<string>> GetRolesAsync(ApplicationUser user)
        => await mediator.Send(new GetRolesQuery(user));

    public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        => await mediator.Send(new CheckPasswordQuery(user, password));
    
    public async Task<IEnumerable<GetUserDto>> GetAllAsync()
        => await mediator.Send(new GetAllUsersQuery());

    public async Task<GetUserDto?> GetByIdAsync(Guid userId)
        => await mediator.Send(new GetByIdQuery(userId));

    public async Task<ServiceResponse> CreateUserAsync(RegisterDto registerDto)
        => await mediator.Send(new CreateUserCommand { RegisterDto = registerDto });
}