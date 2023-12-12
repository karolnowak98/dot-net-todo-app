using MediatR;
using TodoApp.API.Core.Data.Identity;

namespace TodoApp.API.UseCases.Users.Queries;

internal record GetByEmailQuery(string Email) : IRequest<ApplicationUser?>;