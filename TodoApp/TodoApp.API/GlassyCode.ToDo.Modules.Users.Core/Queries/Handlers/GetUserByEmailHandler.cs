using GlassyCode.ToDo.Abstractions.Queries;
using GlassyCode.ToDo.Modules.Users.Core.DAL;
using GlassyCode.ToDo.Modules.Users.Core.DTOs;
using GlassyCode.ToDo.Modules.Users.Core.Exceptions;
using Microsoft.EntityFrameworkCore;

namespace GlassyCode.ToDo.Modules.Users.Core.Queries.Handlers;

internal class GetUserByEmailHandler(UsersDbContext dbContext) : IQueryHandler<GetUserByEmail, UserDetailsDto>
{
    public async Task<UserDetailsDto> HandleAsync(GetUserByEmail query, CancellationToken cancellationToken = default)
    {
        var user = await dbContext.Users
            .AsNoTracking()
            .Include(x => x.Role)
            .SingleOrDefaultAsync(x => x.Email == query.Email, cancellationToken);

        return user?.AsDetailsDto() ?? throw new UserNotFoundException(query.Email);
    }
}