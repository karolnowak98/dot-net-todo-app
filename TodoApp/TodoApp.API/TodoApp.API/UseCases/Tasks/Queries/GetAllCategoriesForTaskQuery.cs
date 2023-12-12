using MediatR;
using TodoApp.API.DTOs.Tasks;

namespace TodoApp.API.UseCases.Tasks.Queries;

internal record GetAllCategoriesForTaskQuery(Guid TaskId) : IRequest<IEnumerable<CategoryDto>>;