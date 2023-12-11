using Microsoft.Extensions.DependencyInjection;
using GlassyCode.ToDo.Modules.Users.Core.DAL;
using GlassyCode.ToDo.Modules.Users.Core.DAL.Repositories;
using GlassyCode.ToDo.Modules.Users.Core.Repositories;
using GlassyCode.ToDo.Modules.Users.Core.Services;
using GlassyCode.ToDo.Shared.Infrastructure;
using GlassyCode.ToDo.Shared.Infrastructure.MsSql;

namespace GlassyCode.ToDo.Modules.Users.Core;

public static class Extensions
{
    public static IServiceCollection AddCore(this IServiceCollection services)
    {
        return services
            .AddSingleton<IUserRequestStorage, UserRequestStorage>()
            .AddScoped<IUserRepository, UserRepository>()
            .AddScoped<IRoleRepository, RoleRepository>()
            .AddMsSqlServer()
            .AddUnitOfWork<UsersUnitOfWork>()
            .AddInitializer<UsersInitializer>();
    }
}