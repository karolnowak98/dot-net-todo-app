using TodoApp.API.Core.Data.Identity;
using TodoApp.API.DTOs;
using TodoApp.API.DTOs.Users;
using TodoApp.API.Exceptions;
using TodoApp.API.Interfaces;

namespace TodoApp.API.Core.Data.Repositories;

public class UsersRepository(UserManager<ApplicationUser> manager, 
    ApplicationDbContext context, IMapper mapper) : IUsersRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new DbContextNullException(nameof(context));
    
    public async Task<ApplicationUser?> GetByEmailAsync(string email)
        => await manager.Users.FirstOrDefaultAsync(u => u.Email == email);

    public async Task<IEnumerable<string>> GetRolesAsync(ApplicationUser user)
        => await manager.GetRolesAsync(user);
    
    public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        => await manager.CheckPasswordAsync(user, password);
    
    public async Task<IEnumerable<GetUserDto>> GetAllAsync()
    {
        var users = await _context.Users.ToListAsync();

        return users.Select(mapper.Map<GetUserDto>);
    }

    public async Task<GetUserDto?> GetByIdAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);

        return mapper.Map<GetUserDto>(user);
    }

    public async Task<ServiceResponse> CreateUserAsync(RegisterDto registerDto)
    {
        var user = mapper.Map<ApplicationUser>(registerDto);
        user.SecurityStamp = Guid.NewGuid().ToString();
        user.UserName = registerDto.Email;

        var result = await manager.CreateAsync(user, registerDto.Password);

        if (!result.Succeeded)
        {
            var errorMessage = string.Join(Environment.NewLine, result.Errors.Select(error => "# " + error.Description));

            return new ServiceResponse
            {
                Success = false,
                Message = "User creation failed because:" + Environment.NewLine + errorMessage
            };
        }

        await manager.AddToRoleAsync(user, StaticUserRoles.USER);

        return new ServiceResponse();
    }
}