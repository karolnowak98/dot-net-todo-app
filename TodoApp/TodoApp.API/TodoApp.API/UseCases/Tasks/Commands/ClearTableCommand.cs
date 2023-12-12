using MediatR;

namespace TodoApp.API.UseCases.Tasks.Commands;

internal record ClearTableCommand : IRequest<bool>;