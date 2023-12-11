using Microsoft.Extensions.DependencyInjection;

namespace GlassyCode.ToDo.Abstractions.Modules;

public interface IModule
{
    string Name { get; }
    void Register(IServiceCollection services);
}