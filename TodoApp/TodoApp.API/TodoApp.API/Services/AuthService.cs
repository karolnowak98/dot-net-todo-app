using TodoApp.API.DTOs;
using TodoApp.API.DTOs.Users;
using TodoApp.API.Exceptions.Jwt;
using TodoApp.API.Interfaces;

namespace TodoApp.API.Services;

public class AuthService(IUsersRepository repository, IConfiguration configuration) : IAuthService
{
    private const string IssuerKey = "JWT:ValidIssuer";
    private const string AudienceKey = "JWT:ValidAudience";
    private const string SecretKey = "JWT:Secret";
    
    private string GenerateNewJsonWebToken(IEnumerable<Claim> claims)
    {
        var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration[SecretKey] ?? "defaultSecret"));
        var issuer = configuration[IssuerKey] ?? "defaultIssuer";
        var audience = configuration[AudienceKey] ?? "defaultAudience";
        
        var tokenObj = new JwtSecurityToken(
            issuer: issuer,
            audience: audience,
            expires: DateTime.Now.AddHours(1),
            claims: claims,
            signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
        );

        var token = new JwtSecurityTokenHandler().WriteToken(tokenObj);
            
        return token;
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