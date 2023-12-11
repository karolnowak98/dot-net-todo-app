using GlassyCode.ToDo.Shared.Infrastructure.MsSql;

namespace GlassyCode.ToDo.Modules.Users.Core.DAL;

internal class UsersUnitOfWork : MsSqlUnitOfWork<UsersDbContext>
{
    public UsersUnitOfWork(UsersDbContext dbContext) : base(dbContext)
    {
        
    }
}