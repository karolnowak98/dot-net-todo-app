using TodoApp.API.DTOs;
using TodoApp.API.DTOs.Users;

namespace TodoApp.API.Interfaces;

public interface IAuthService
{
    public Task<ServiceResponse> RegisterAsync(RegisterDto registerDto);
    public Task<ServiceResponse<string>> LoginAsync(LoginDto loginDto);
}