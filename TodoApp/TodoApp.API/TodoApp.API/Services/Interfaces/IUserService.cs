using TodoApp.API.Models.User.Dto;

namespace TodoApp.API.Services.Interfaces
{
    public interface IUserService
    {
        public Task<ServiceResponse<IEnumerable<GetUserDto>>> GetAllUsers();
        public Task<ServiceResponse<GetUserDto>> GetUserById(Guid id);
        public Task<ServiceResponse> RegisterUser(RegisterDto registerDto);
        public Task<ServiceResponse<string>> Login(LoginDto loginDto);
    }
}