using MediatR;
using TodoApp.API.Models.TaskCategory;

namespace TodoApp.API.UseCases.Tasks.Queries;

internal record GetTaskCategoriesForTaskQuery(Guid TaskId) : IRequest<IEnumerable<TaskCategory>>;