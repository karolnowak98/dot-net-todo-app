using TodoApp.API.Core.Models;
using TodoApp.API.Models.User.Dto;

namespace TodoApp.API.Repositories.Interfaces
{
    public interface IUserRepository
    {
        public Task<IEnumerable<GetUserDto>> GetAll();
        public Task<GetUserDto?> GetUserById(Guid id);
        public Task<ApplicationUser?> GetUserByEmail(string email);
        public Task<IEnumerable<string>> GetRoles(ApplicationUser user);
        public Task<ServiceResponse> CreateUser(RegisterDto registerDto);
        public Task<bool> CheckPassword(ApplicationUser user, string password);
    }
}