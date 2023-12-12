using MediatR;
using TodoApp.API.Core.Data.Identity;

namespace TodoApp.API.UseCases.Users.Queries;

internal record CheckPasswordQuery(ApplicationUser User, string Password) : IRequest<bool>;
