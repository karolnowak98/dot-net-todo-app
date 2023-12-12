using TodoApp.API.DTOs;
using TodoApp.API.DTOs.Users;
using TodoApp.API.Interfaces;

namespace TodoApp.API.Services;

public class UsersService(IUsersRepository repository) : IUsersService
{
    public async Task<ServiceResponse<IEnumerable<GetUserDto>>> GetAllAsync()
    {
        var users = await repository.GetAllAsync();
        return new ServiceResponse<IEnumerable<GetUserDto>> { Data = users };
    }
        
    public async Task<ServiceResponse<GetUserDto>> GetByIdAsync(Guid id)
    {
        var user = await repository.GetByIdAsync(id);

        if (user is null)
        {
            return new ServiceResponse<GetUserDto>
            {
                Success = false,
                Message = "User not found!!"
            };
        }

        return new ServiceResponse<GetUserDto> { Data = user };
    }
}