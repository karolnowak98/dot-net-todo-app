using TaskStatus = TodoApp.API.Models.Task.Enums.TaskStatus;

namespace TodoApp.API.Models.Task
{
    public class UpdateStatusTaskDto
    {
        public Guid Id { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        public TaskStatus Status { get; set; }
    }
}