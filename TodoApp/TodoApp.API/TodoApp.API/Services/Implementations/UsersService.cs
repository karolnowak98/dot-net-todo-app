using TodoApp.API.Models.User.Dto;

namespace TodoApp.API.Services.Implementations
{
    public class UsersService : IUsersService
    {
        public UsersService(IUsersRepository repository, IConfiguration configuration)
        {
            _repository = repository;
            _configuration = configuration;
        }
        
        private readonly IUsersRepository _repository;
        private readonly IConfiguration _configuration;
        
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
        
        public async Task<ServiceResponse<IEnumerable<GetUserDto>>> GetAllAsync()
        {
            var users = await _repository.GetAllAsync();
            return new ServiceResponse<IEnumerable<GetUserDto>> { Data = users };
        }
        
        public async Task<ServiceResponse<GetUserDto>> GetByIdAsync(Guid id)
        {
            var user = await _repository.GetByIdAsync(id);

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
            return await _repository.CreateUserAsync(registerDto);
        }

        public async Task<ServiceResponse<string>> LoginAsync(LoginDto loginDto)
        {
            var user = await _repository.GetByEmailAsync(loginDto.Email);

            if (user is null)
            {
                return new ServiceResponse<string>
                {
                    Message = "Invalid credentials!",
                    Success = false
                };
            }

            var isPasswordCorrect = await _repository.CheckPasswordAsync(user, loginDto.Password);

            if (!isPasswordCorrect)
            {
                return new ServiceResponse<string>
                {
                    Message = "Invalid credentials!",
                    Success = false
                };
            }

            var userRoles = await _repository.GetRolesAsync(user);

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
}