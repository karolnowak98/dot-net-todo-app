using GlassyCode.ToDo.Abstractions.Commands;
using GlassyCode.ToDo.Abstractions.Events;
using GlassyCode.ToDo.Abstractions.Queries;

namespace GlassyCode.ToDo.Abstractions.Dispatchers;

public interface IDispatcher
{
    Task SendAsync<T>(T command, CancellationToken cancellationToken = default) where T : class, ICommand;
    Task PublishAsync<T>(T @event, CancellationToken cancellationToken = default) where T : class, IEvent;
    Task<TResult> QueryAsync<TResult>(IQuery<TResult> query, CancellationToken cancellationToken = default);
}