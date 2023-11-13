using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TodoApp.API.Models.User.Dto;

namespace TodoApp.API.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IConfiguration _configuration;

        public UserService(IUserRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }
        
        public async Task<ServiceResponse<IEnumerable<GetUserDto>>> GetUsers()
        {
            var users = await _repository.GetAll();
            return new ServiceResponse<IEnumerable<GetUserDto>> { Data = users };
        }
        
        public async Task<ServiceResponse<GetUserDto>> GetUserById(Guid id)
        {
            var user = await _repository.GetUserById(id);

            if (user == null)
            {
                return new ServiceResponse<GetUserDto>
                {
                    Success = false,
                    Message = "User not found!!"
                };
            }

            return new ServiceResponse<GetUserDto> { Data = user };
        }
        
        public async Task<ServiceResponse> Register(RegisterDto registerDto)
        {
            return await _repository.CreateUser(registerDto);
        }

        public async Task<ServiceResponse<string>> Login(LoginDto loginDto)
        {
            var user = await _repository.GetUserByEmail(loginDto.Email);

            if (user is null)
            {
                return new ServiceResponse<string>
                {
                    Message = "Invalid credentials!",
                    Success = false
                };
            }

            var isPasswordCorrect = await _repository.CheckPassword(user, loginDto.Password);

            if (!isPasswordCorrect)
            {
                return new ServiceResponse<string>
                {
                    Message = "Invalid credentials!",
                    Success = false
                };
            }

            var userRoles = await _repository.GetRoles(user);

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

        private string GenerateNewJsonWebToken(IEnumerable<Claim> claims)
        {
            var authSecret = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"] ?? string.Empty));

            var tokenObj = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidAudience"],
                expires: DateTime.Now.AddHours(1),
                claims: claims,
                signingCredentials: new SigningCredentials(authSecret, SecurityAlgorithms.HmacSha256)
                );

            var token = new JwtSecurityTokenHandler().WriteToken(tokenObj);
            
            return token;
        }
    }
}