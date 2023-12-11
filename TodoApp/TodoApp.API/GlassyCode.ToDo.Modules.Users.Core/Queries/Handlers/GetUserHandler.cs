using GlassyCode.ToDo.Abstractions.Queries;
using GlassyCode.ToDo.Modules.Users.Core.DAL;
using GlassyCode.ToDo.Modules.Users.Core.DTOs;
using GlassyCode.ToDo.Modules.Users.Core.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace GlassyCode.ToDo.Modules.Users.Core.Queries.Handlers;

internal class GetUserHandler(UsersDbContext usersDbContext) : IQueryHandler<GetUser, UserDetailsDto>
{
    public async Task<UserDetailsDto> HandleAsync(GetUser query, CancellationToken cancellationToken = default)
    {
        var user = await usersDbContext.Users
            .AsNoTracking()
            .Include(user => user.Role)
            .SingleOrDefaultAsync(user => user.Id == query.UserId, cancellationToken);

        return user?.AsDetailsDto() ?? throw new UserNotFoundException(query.UserId);
    }
}