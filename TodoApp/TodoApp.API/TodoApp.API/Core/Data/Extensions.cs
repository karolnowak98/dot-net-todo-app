using TodoApp.API.Interfaces;

namespace TodoApp.API.Core.Data;

public static class Extensions
{
    public static async Task<bool> SaveChangesAsyncCheck(this IApplicationDbContext dbContext, CancellationToken ct)
        => await dbContext.SaveChangesAsync(ct) > 0;
}