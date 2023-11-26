using TodoApp.API.Core.Models;
using TodoApp.API.Models.User.Dto;

namespace TodoApp.API.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        public Task<IEnumerable<GetUserDto>> GetAllAsync();
        public Task<GetUserDto?> GetByIdAsync(Guid id);
        public Task<ApplicationUser?> GetByEmailAsync(string email);
        public Task<IEnumerable<string>> GetRolesAsync(ApplicationUser user);
        public Task<ServiceResponse> CreateUserAsync(RegisterDto registerDto);
        public Task<bool> CheckPasswordAsync(ApplicationUser user, string password);
    }
}