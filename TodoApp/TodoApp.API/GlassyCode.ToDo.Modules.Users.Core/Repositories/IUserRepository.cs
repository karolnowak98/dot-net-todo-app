using GlassyCode.ToDo.Modules.Users.Core.Entities;

namespace GlassyCode.ToDo.Modules.Users.Core.Repositories;

internal interface IUserRepository
{
    Task<User?> GetAsync(string email);
    Task<User?> GetAsync(Guid id);
    Task AddAsync(User user);
    Task UpdateAsync(User user);
}