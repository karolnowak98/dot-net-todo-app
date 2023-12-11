using TodoApp.API.Models.Task;
using TaskStatus = TodoApp.API.Models.Task.TaskStatus;

namespace TodoApp.API.DTOs.Tasks;

public class GetTaskDto
{
    public Guid Id { get; init; }
    public string Title { get; init; }
    public string Description { get; init; }
    public DateTime Deadline { get; init; }
    public TaskPriority Priority { get; init; }
    public TaskStatus Status { get; init; }
    public IEnumerable<CategoryDto> Categories { get; set; }
}