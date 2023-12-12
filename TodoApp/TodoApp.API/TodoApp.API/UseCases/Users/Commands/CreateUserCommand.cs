using MediatR;
using TodoApp.API.DTOs;
using TodoApp.API.DTOs.Users;

namespace TodoApp.API.UseCases.Users.Commands;

internal record CreateUserCommand(RegisterDto RegisterDto) : IRequest<ServiceResponse>;