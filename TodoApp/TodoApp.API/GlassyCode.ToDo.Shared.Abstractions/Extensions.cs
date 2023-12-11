namespace GlassyCode.ToDo.Abstractions;

public static class Extensions
{
    public static async Task<T> NotNull<T>(this Task<T> task, Func<Exception> exception = null)
    {
        if (task is null)
        {
            throw new InvalidOperationException("Task cannot be null.");
        }

        var result = await task;

        if (result is null)
        {
            throw new InvalidOperationException("Returned result is null.");
        }

        if (exception is not null)
        {
            throw exception();
        }

        return result;
    }
}