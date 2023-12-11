using TodoApp.API.DTOs;
using TodoApp.API.DTOs.Tasks;
using TodoApp.API.Interfaces;

namespace TodoApp.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TasksController(ITasksService service) : ControllerBase
{
    [Authorize(Roles = StaticUserRoles.USER)]
    [HttpGet("get-all")] 
    public async Task<ActionResult<ServiceResponse<IEnumerable<GetTaskDto>>>> GetAll()
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
        var response = await service.GetTasksForUserAsync(userId);

        return response.Success ? Ok(response) : BadRequest(response);
    }
        
    [Authorize(Roles = StaticUserRoles.USER)]
    [HttpPost("create-task")]
    public async Task<IActionResult> CreateTask([FromBody] GetTaskDto getTaskDto)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
        var response = await service.CreateTaskAsync(userId, getTaskDto);

        return response.Success ? Ok(response) : BadRequest(response);
    }
        
    [Authorize(Roles = StaticUserRoles.USER)]
    [HttpPut("update-task-status")]
    public async Task<ActionResult<ServiceResponse<bool>>> UpdateTaskStatus([FromBody] UpdateStatusTaskDto updateStatusTaskDto)
    {
        var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier) ?? string.Empty);
        var response = await service.UpdateTaskStatusAsync(userId, updateStatusTaskDto);

        return response.Success ? Ok(response) : BadRequest(response);
    }
}