using MediatR;
using TodoApp.API.DTOs;
using TodoApp.API.DTOs.Users;

namespace TodoApp.API.UseCases.Users.Commands;

public class CreateUserCommand : IRequest<ServiceResponse>
{
    public RegisterDto RegisterDto { get; set; }
}