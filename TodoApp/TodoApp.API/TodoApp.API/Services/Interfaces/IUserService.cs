using TodoApp.API.Models.User.Dto;

namespace TodoApp.API.Services.Interfaces
{
    public interface IUserService
    {
        public Task<ServiceResponse<IEnumerable<GetUserDto>>> GetUsers();
        public Task<ServiceResponse<GetUserDto>> GetUserById(Guid id);
        public Task<ServiceResponse> Register(RegisterDto registerDto);
        public Task<ServiceResponse<string>> Login(LoginDto loginDto);
    }
}