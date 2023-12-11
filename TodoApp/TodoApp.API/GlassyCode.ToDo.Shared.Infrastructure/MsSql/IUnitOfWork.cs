namespace GlassyCode.ToDo.Shared.Infrastructure.MsSql;

public interface IUnitOfWork
{
    Task ExecuteAsync(Func<Task> action);
}