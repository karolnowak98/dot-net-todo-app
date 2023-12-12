using TodoApp.API.DTOs;
using TodoApp.API.DTOs.Tasks;
using TodoApp.API.Interfaces;

namespace TodoApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController(ITasksService service) : ControllerBase
{
    [Authorize(Roles = StaticUserRoles.User)]
    [HttpGet("get-all")] 
    [ProducesResponseType(typeof(ServiceResponse<IEnumerable<GetTaskDto>>),StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ServiceResponse<IEnumerable<GetTaskDto>>),StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceResponse<IEnumerable<GetTaskDto>>>> GetAll()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
        var response = await service.GetTasksForUserAsync(userId);

        return response.Success ? Ok(response) : BadRequest(response);
    }
        
    [Authorize(Roles = StaticUserRoles.User)]
    [HttpPost("create-task")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateTask([FromBody] GetTaskDto getTaskDto)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
        var response = await service.CreateTaskAsync(userId, getTaskDto);

        return response.Success ? Ok(response) : BadRequest(response);
    }
        
    [Authorize(Roles = StaticUserRoles.User)]
    [HttpPut("update-task-status")]
    [ProducesResponseType(typeof(bool), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(bool),StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<ServiceResponse<bool>>> UpdateTaskStatus([FromBody] UpdateStatusTaskDto updateStatusTaskDto)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
        var response = await service.UpdateTaskStatusAsync(userId, updateStatusTaskDto);

        return response.Success ? Ok(response) : BadRequest(response);
    }
}