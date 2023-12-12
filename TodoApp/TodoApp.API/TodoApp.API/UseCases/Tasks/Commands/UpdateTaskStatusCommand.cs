using MediatR;
using TodoApp.API.DTOs.Tasks;

namespace TodoApp.API.UseCases.Tasks.Commands;

internal record UpdateTaskStatusCommand(Guid UserId, 
    UpdateStatusTaskDto UpdateStatusTaskDto) : IRequest<bool>;