using TodoApp.API.Core.Data.Identity;
using TodoApp.API.DTOs;
using TodoApp.API.DTOs.Users;
using TodoApp.API.Interfaces;

namespace TodoApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService,
    RoleManager<ApplicationRole> roleManager) : ControllerBase
{
    [HttpPost("seed-roles")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<IActionResult> SeedRoles()
    {
        var isOwnerRoleExists = await roleManager.RoleExistsAsync(StaticUserRoles.Owner);
        var isAdminRoleExists = await roleManager.RoleExistsAsync(StaticUserRoles.Admin);
        var isUserRoleExists = await roleManager.RoleExistsAsync(StaticUserRoles.User);

        if (isOwnerRoleExists && isAdminRoleExists && isUserRoleExists) return Ok("Roles seeding is already done!");
            
        await roleManager.CreateAsync(new ApplicationRole { Name = StaticUserRoles.User });
        await roleManager.CreateAsync(new ApplicationRole { Name = StaticUserRoles.Admin });
        await roleManager.CreateAsync(new ApplicationRole { Name = StaticUserRoles.Owner });

        return Ok("Role seeding done successfully!");
    }

    [HttpPost("register")]
    [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse), StatusCodes.Status409Conflict)]
    public async Task<ActionResult<ServiceResponse>> Register([FromBody] RegisterDto userDto)
    {
        var response = await authService.RegisterAsync(userDto);
        
        return response.Success ? Ok(response) : Conflict(response);
    }
    [ProducesDefaultResponseType]
    [HttpPost("login")]
    [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<string>), StatusCodes.Status401Unauthorized)]
    public async Task<ActionResult<ServiceResponse<string>>> Login([FromBody] LoginDto loginDto)
    {
        var response = await authService.LoginAsync(loginDto);

        return response.Success ? Ok(response) : Unauthorized(response);
    }
}