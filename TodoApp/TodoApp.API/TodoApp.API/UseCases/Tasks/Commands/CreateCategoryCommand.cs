using MediatR;
using TodoApp.API.Models.Category;

namespace TodoApp.API.UseCases.Tasks.Commands;

internal record CreateCategoryCommand(Category Category) : IRequest<bool>;