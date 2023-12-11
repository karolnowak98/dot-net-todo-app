using GlassyCode.ToDo.Modules.Tasks.Core.Entities.Enums;
using TaskStatus = GlassyCode.ToDo.Modules.Tasks.Core.Entities.Enums.TaskStatus;

namespace GlassyCode.ToDo.Modules.Tasks.Core.Entities;

public class Task
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime Deadline { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TaskPriority Priority { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TaskStatus Status { get; set; }
}