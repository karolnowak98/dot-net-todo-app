using TodoApp.API.Core.Data.Identity;
using TodoApp.API.DTOs;
using TodoApp.API.DTOs.Users;

namespace TodoApp.API.Interfaces;

public interface IUsersRepository
{
    public Task<IEnumerable<GetUserDto>> GetAllAsync();
    public Task<GetUserDto?> GetByIdAsync(Guid userId);
    public Task<ApplicationUser?> GetByEmailAsync(string email);
    public Task<IEnumerable<string>> GetRolesAsync(ApplicationUser user);
    public Task<ServiceResponse> CreateUserAsync(RegisterDto registerDto);
    public Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
}