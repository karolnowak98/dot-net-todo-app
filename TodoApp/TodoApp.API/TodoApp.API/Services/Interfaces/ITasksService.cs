using TodoApp.API.Models.Task;

namespace TodoApp.API.Services.Interfaces
{
    public interface ITasksService
    {
        public Task<ServiceResponse<IEnumerable<TaskDto>>> GetTasksForUserAsync(Guid userId);
        public Task<ServiceResponse> CreateTaskAsync(Guid userId, TaskDto taskDto);
    }
}