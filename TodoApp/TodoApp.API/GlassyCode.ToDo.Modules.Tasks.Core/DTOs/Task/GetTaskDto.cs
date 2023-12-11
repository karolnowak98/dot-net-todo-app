using GlassyCode.ToDo.Modules.Tasks.Core.Entities.Enums;
using TaskStatus = GlassyCode.ToDo.Modules.Tasks.Core.Entities.Enums.TaskStatus;

namespace GlassyCode.ToDo.Modules.Tasks.Core.DTOs.Task;

public class GetTaskDto
{
    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    public TaskPriority Priority { get; set; }
    public TaskStatus Status { get; set; }
    public IEnumerable<CategoryDto> Categories { get; set; }
}