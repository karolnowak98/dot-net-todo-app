using TaskStatus = GlassyCode.ToDo.Modules.Tasks.Core.Entities.Enums.TaskStatus;

namespace GlassyCode.ToDo.Modules.Tasks.Core.DTOs.Task;

public class UpdateStatusTaskDto
{
    public Guid Id { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TaskStatus Status { get; set; }
}