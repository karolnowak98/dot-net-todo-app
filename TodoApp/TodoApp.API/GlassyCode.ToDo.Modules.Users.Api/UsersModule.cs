using GlassyCode.ToDo.Abstractions.Modules;
using GlassyCode.ToDo.Abstractions.Queries;
using GlassyCode.ToDo.Modules.Users.Core;
using GlassyCode.ToDo.Modules.Users.Core.DTOs;
using GlassyCode.ToDo.Modules.Users.Core.Queries;
using GlassyCode.ToDo.Shared.Infrastructure;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace GlassyCode.ToDo.Modules.Users.Api;

internal class UsersModule : IModule
{
    public string Name { get; } = "Users";
        
    public IEnumerable<string> Policies { get; } = new[]
    {
        "users"
    };

    public void Register(IServiceCollection services)
    {
        services.AddCore();
    }
        
    public void Use(IApplicationBuilder app)
    {
        app.UseModuleRequests()
            .Subscribe<GetUserByEmail, UserDetailsDto>("users/get",
                (query, serviceProvider, cancellationToken) =>
                    serviceProvider.GetRequiredService<IQueryDispatcher>().QueryAsync(query, cancellationToken));
    }
}