using TodoApp.API.Models.Task;
using Task = TodoApp.API.Models.Task.Task;
using TaskStatus = TodoApp.API.Models.Task.Enums.TaskStatus;

namespace TodoApp.API.Repositories.Interfaces
{
    public interface ITasksRepository
    {
        public Task<IEnumerable<Task>> GetAllAsync(Guid userId);
        public Task<Task?> GetByIdAsync(Guid taskId);
        public Task<bool> CreateTaskAsync(Task task);
        public Task<bool> CreateTaskCategoryAsync(Guid taskId, Guid categoryId);
        public Task<bool> UpdateTaskStatusAsync(Guid userId, UpdateStatusTaskDto updateStatusTaskDto);
    }
}