using Microsoft.Extensions.DependencyInjection;

namespace GlassyCode.ToDo.Shared.Infrastructure.MsSql;

public static class Extensions
{
    public static IServiceCollection AddMsSqlServer(this IServiceCollection services)
    {
        var options = services.GetOptions<MsSqlOptions>("connectionString");
        services.AddSingleton(options);
        
        // services.AddSingleton(new UnitOfWorkTypeRegistry());
        //     
        // // Temporary fix for EF Core issue related to https://github.com/npgsql/efcore.pg/issues/2000
        // AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        
        return services;
    }
    
    public static IServiceCollection AddUnitOfWork<T>(this IServiceCollection services) where T : class, IUnitOfWork
    {
        services.AddScoped<IUnitOfWork, T>();
        services.AddScoped<T>();
        using var serviceProvider = services.BuildServiceProvider();
        serviceProvider.GetRequiredService<UnitOfWorkTypeRegistry>().Register<T>();

        return services;
    }
}