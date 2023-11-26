using TodoApp.API.Models.User.Dto;

namespace TodoApp.API.Services.Interfaces
{
    public interface IUsersService
    {
        public Task<ServiceResponse<IEnumerable<GetUserDto>>> GetAllAsync();
        public Task<ServiceResponse<GetUserDto>> GetByIdAsync(Guid id);
        public Task<ServiceResponse> RegisterAsync(RegisterDto registerDto);
        public Task<ServiceResponse<string>> LoginAsync(LoginDto loginDto);
    }
}