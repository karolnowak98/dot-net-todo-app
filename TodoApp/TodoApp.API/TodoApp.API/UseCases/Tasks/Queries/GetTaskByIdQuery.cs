using MediatR;
using Task = TodoApp.API.Models.Task.Task;

namespace TodoApp.API.UseCases.Tasks.Queries;

internal record GetTaskByIdQuery(Guid TaskId) : IRequest<Task?>;