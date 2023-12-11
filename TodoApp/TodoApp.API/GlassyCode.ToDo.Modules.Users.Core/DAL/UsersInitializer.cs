using GlassyCode.ToDo.Modules.Users.Core.Entities;
using GlassyCode.ToDo.Shared.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace GlassyCode.ToDo.Modules.Users.Core.DAL;

internal sealed class UsersInitializer : IInitializer
{
    private readonly HashSet<string> _permissions =
    [
        "customers",
        "deposits", "withdrawals",
        "users",
        "transfers", "wallets"
    ];
    
    private readonly UsersDbContext _dbContext;
    private readonly ILogger<UsersInitializer> _logger;

    public UsersInitializer(UsersDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task InitAsync()
    {
        if (await _dbContext.Roles.AnyAsync())
        {
            return;
        }

        await AddRolesAsync();
        await _dbContext.SaveChangesAsync();
    }
    
    private async Task AddRolesAsync()
    {
        await _dbContext.Roles.AddAsync(new Role
        {
            Name = "admin",
            Permissions = _permissions
        });
        await _dbContext.Roles.AddAsync(new Role
        {
            Name = "user",
            Permissions = new List<string>()
        });
        
        _logger.LogInformation("Initialized roles.");
    }
}