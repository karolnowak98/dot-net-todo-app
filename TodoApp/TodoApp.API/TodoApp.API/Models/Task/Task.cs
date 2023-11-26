using TodoApp.API.Models.Task.Enums;
using TaskStatus = TodoApp.API.Models.Task.Enums.TaskStatus;

namespace TodoApp.API.Models.Task
{
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
}