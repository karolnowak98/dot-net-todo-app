using TodoApp.API.Core.Models;
using TodoApp.API.Models.User.Dto;

namespace TodoApp.API.Repositories.Implementations
{
    public class UsersRepository : IUsersRepository
    {
        public UsersRepository(UserManager<ApplicationUser> manager, ApplicationDbContext context, IMapper mapper)
        {
            _manager = manager;
            _context = context;
            _mapper = mapper;
        }
        
        private readonly UserManager<ApplicationUser> _manager;
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public async Task<IEnumerable<GetUserDto>> GetAllAsync()
        {
            var users = await _context.Users.ToListAsync();

            return users.Select(u => _mapper.Map<GetUserDto>(u));
        }

        public async Task<GetUserDto?> GetByIdAsync(Guid id)
        {
            var user = await _context.Users.FindAsync(id);

            return _mapper.Map<GetUserDto>(user);
        }

        public async Task<ApplicationUser?> GetByEmailAsync(string email)
        {
            return await _manager.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<IEnumerable<string>> GetRolesAsync(ApplicationUser user)
        {
            return await _manager.GetRolesAsync(user);
        }

        public async Task<ServiceResponse> CreateUserAsync(RegisterDto registerDto)
        {
            var user = _mapper.Map<ApplicationUser>(registerDto);
            user.SecurityStamp = Guid.NewGuid().ToString();
            user.UserName = registerDto.Email;

            var result = await _manager.CreateAsync(user, registerDto.Password);

            if (!result.Succeeded)
            {
                var errorMessage = string.Join(Environment.NewLine, result.Errors.Select(error => "# " + error.Description));

                return new ServiceResponse
                {
                    Success = false,
                    Message = "User creation failed because:" + Environment.NewLine + errorMessage
                };
            }

            await _manager.AddToRoleAsync(user, StaticUserRoles.USER);

            return new ServiceResponse();
        }

        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string password)
        {
            return await _manager.CheckPasswordAsync(user, password);
        }
    }
}