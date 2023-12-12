using MediatR;
using TodoApp.API.Models.Category;

namespace TodoApp.API.UseCases.Tasks.Queries;

internal record GetCategoryByIdQuery(Guid CategoryId) : IRequest<Category?>;