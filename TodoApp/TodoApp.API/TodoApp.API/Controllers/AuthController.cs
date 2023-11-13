using TodoApp.API.Core.Models;
using TodoApp.API.Models.User.Dto;

namespace TodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly RoleManager<ApplicationRole> _roleManager;
        
        public AuthController(IUserService userService, RoleManager<ApplicationRole> roleManager)
        {
            _userService = userService;
            _roleManager = roleManager;
        }

        [HttpPost("seed-roles")]
        public async Task<IActionResult> SeedRoles()
        {
            var isOwnerRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.OWNER);
            var isAdminRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.ADMIN);
            var isUserRoleExists = await _roleManager.RoleExistsAsync(StaticUserRoles.USER);

            if (isOwnerRoleExists && isAdminRoleExists && isUserRoleExists) return Ok("Roles seeding is already done!");
            
            await _roleManager.CreateAsync(new ApplicationRole { Name = StaticUserRoles.USER });
            await _roleManager.CreateAsync(new ApplicationRole { Name = StaticUserRoles.ADMIN });
            await _roleManager.CreateAsync(new ApplicationRole { Name = StaticUserRoles.OWNER });

            return Ok("Role seeding done successfully!");
        }

        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse>> Register([FromBody] RegisterDto userDto)
        {
            var response = await _userService.Register(userDto);
        
            return response.Success ? Ok(response) : Conflict(response);
        }

        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login([FromBody] LoginDto loginDto)
        {
            var response = await _userService.Login(loginDto);

            return response.Success ? Ok(response) : Unauthorized(response);
        }
    }
}