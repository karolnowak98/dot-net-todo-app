using TodoApp.API.DTOs;
using TodoApp.API.DTOs.Users;
using TodoApp.API.Interfaces;

namespace TodoApp.API.Services;

public class UsersService(IUsersRepository repository, IConfiguration configuration) : IUsersService
{
    private string GenerateNewJsonWebToken(IEnumerable<Claim> claims)
    {
        var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"] ?? string.Empty));

        var tokenObj = new JwtSecurityToken(
            issuer: configuration["JWT:ValidIssuer"],
            audience: configuration["JWT:ValidAudience"],
            expires: DateTime.Now.AddHours(1),
            claims: claims,
            signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
        );

        var token = new JwtSecurityTokenHandler().WriteToken(tokenObj);
            
        return token;
    }
        
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
        
    public async Task<ServiceResponse> RegisterAsync(RegisterDto registerDto)
    {
        return await repository.CreateUserAsync(registerDto);
    }

    public async Task<ServiceResponse<string>> LoginAsync(LoginDto loginDto)
    {
        var user = await repository.GetByEmailAsync(loginDto.Email);

        if (user is null)
        {
            return new ServiceResponse<string>
            {
                Message = "Invalid credentials!",
                Success = false
            };
        }

        var isPasswordCorrect = await repository.CheckPasswordAsync(user, loginDto.Password);

        if (!isPasswordCorrect)
        {
            return new ServiceResponse<string>
            {
                Message = "Invalid credentials!",
                Success = false
            };
        }

        var userRoles = await repository.GetRolesAsync(user);

        var authClaims = new List<Claim>
        {
            new(ClaimTypes.Name, user.Email),
            new(ClaimTypes.NameIdentifier, user.Id),
            new("JWTID", Guid.NewGuid().ToString())
        };
            
        authClaims.AddRange(userRoles.Select(role => new Claim(ClaimTypes.Role, role)));

        var token = GenerateNewJsonWebToken(authClaims);

        return new ServiceResponse<string> { Data = token };
    }
}