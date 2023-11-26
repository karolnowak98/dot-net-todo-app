using TodoApp.API.Models.Category;
using TodoApp.API.Models.Task.Enums;
using TaskStatus = TodoApp.API.Models.Task.Enums.TaskStatus;

namespace TodoApp.API.Models.Task
{
    public class TaskDto
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Deadline { get; set; }
        public TaskPriority Priority { get; set; }
        public TaskStatus Status { get; set; }
        public IEnumerable<CategoryDto> Categories { get; set; }
    }
}