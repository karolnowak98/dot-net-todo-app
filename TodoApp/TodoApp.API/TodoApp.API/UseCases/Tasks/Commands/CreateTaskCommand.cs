using MediatR;
using Task = TodoApp.API.Models.Task.Task;

namespace TodoApp.API.UseCases.Tasks.Commands;

internal record CreateTaskCommand(Task Task) : IRequest<bool>;
