using MediatR;

namespace TodoApp.API.UseCases.Tasks.Commands;

internal record CreateTaskCategoryCommand(Guid TaskId, Guid CategoryId) : IRequest<bool>;