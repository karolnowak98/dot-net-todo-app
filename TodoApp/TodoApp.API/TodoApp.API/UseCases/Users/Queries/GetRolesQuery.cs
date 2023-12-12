using MediatR;
using TodoApp.API.Core.Data.Identity;

namespace TodoApp.API.UseCases.Users.Queries;

internal record GetRolesQuery(ApplicationUser User) : IRequest<IEnumerable<string>>;