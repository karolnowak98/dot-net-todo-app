using MediatR;
using TodoApp.API.DTOs.Users;

namespace TodoApp.API.UseCases.Users.Queries;

internal record GetAllUsersQuery : IRequest<IEnumerable<GetUserDto>>;
