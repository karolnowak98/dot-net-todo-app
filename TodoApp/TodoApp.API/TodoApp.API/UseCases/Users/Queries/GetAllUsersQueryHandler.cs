using MediatR;
using TodoApp.API.DTOs.Users;

namespace TodoApp.API.UseCases.Users.Queries;

internal class GetAllUsersQueryHandler(ApplicationDbContext context, IMapper mapper)
    : IRequestHandler<GetAllUsersQuery, IEnumerable<GetUserDto>>
{
    public async Task<IEnumerable<GetUserDto>> Handle(GetAllUsersQuery request, CancellationToken ct)
    {
        var users = await context.Users.ToListAsync(cancellationToken: ct);

        return users.Select(mapper.Map<GetUserDto>);
    }
}