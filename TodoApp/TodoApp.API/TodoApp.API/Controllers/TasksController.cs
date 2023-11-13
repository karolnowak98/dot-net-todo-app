using TodoApp.API.Models.Task;

namespace TodoApp.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TasksController : ControllerBase
    {
        private readonly ITasksService _service;

        public TasksController(ITasksService service)
        {
            _service = service;
        }

        [Authorize(Roles = StaticUserRoles.USER)]
        [HttpGet("get-tasks")] 
        public async Task<ActionResult<ServiceResponse<IEnumerable<TaskDto>>>> GetTasksForUser()
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
            var response = await _service.GetTasks(userId);

            return response.Success ? Ok(response) : BadRequest(response);
        }
        
        [Authorize(Roles = StaticUserRoles.USER)]
        [HttpPost("add-task")]
        public async Task<IActionResult> AddTask([FromBody] TaskDto taskDto)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
            var response = await _service.AddTask(userId, taskDto);

            return response.Success ? Ok(response) : BadRequest(response);
        }
    }
}