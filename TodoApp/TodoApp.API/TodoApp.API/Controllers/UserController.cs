using TodoApp.API.Models.User.Dto;

namespace TodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        
        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet("GetAllUsers")]
        public async Task<ActionResult<ServiceResponse<IEnumerable<LoginDto>>>> GetAllUsers()
        {
            var response = await _userService.GetAllUsers();

            if (response.Success)
            {
                return Ok(response);    
            }

            return BadRequest(response);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ServiceResponse<LoginDto>>> GetUser(Guid id)
        {
            var response = await _userService.GetUserById(id);

            if (response.Success)
            {
                return Ok(response);    
            }

            return BadRequest(response);
        }
    }
}