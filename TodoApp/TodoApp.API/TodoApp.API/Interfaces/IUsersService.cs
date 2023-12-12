using TodoApp.API.DTOs;
using TodoApp.API.DTOs.Users;

namespace TodoApp.API.Interfaces;

public interface IUsersService
{
    public Task<ServiceResponse<IEnumerable<GetUserDto>>> GetAllAsync();
    public Task<ServiceResponse<GetUserDto>> GetByIdAsync(Guid id);
}