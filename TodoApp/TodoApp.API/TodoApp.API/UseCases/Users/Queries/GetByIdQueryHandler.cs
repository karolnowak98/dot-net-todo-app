using MediatR;
using TodoApp.API.DTOs.Users;

namespace TodoApp.API.UseCases.Users.Queries;

internal class GetByIdQueryHandler(ApplicationDbContext dbContext, IMapper mapper) : IRequestHandler<GetByIdQuery, GetUserDto>
{
    public async Task<GetUserDto> Handle(GetByIdQuery request, CancellationToken ct)
    {
        var user = await dbContext.Users.FindAsync(new object?[] { request.UserId }, cancellationToken: ct);

        return mapper.Map<GetUserDto>(user);
    }
}