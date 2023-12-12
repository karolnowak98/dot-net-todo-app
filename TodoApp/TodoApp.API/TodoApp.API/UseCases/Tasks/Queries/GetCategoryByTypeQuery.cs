using MediatR;
using TodoApp.API.Models.Category;

namespace TodoApp.API.UseCases.Tasks.Queries;

internal record GetCategoryByTypeQuery(CategoryType Type) : IRequest<Category?>;