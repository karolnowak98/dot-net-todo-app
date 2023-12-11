using TaskStatus = TodoApp.API.Models.Task.TaskStatus;

namespace TodoApp.API.DTOs.Tasks;

public class UpdateStatusTaskDto
{
    public Guid Id { get; set; }
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public TaskStatus Status { get; set; }
}