using TodoApp.API.Models.Task;

namespace TodoApp.API.Services.Interfaces
{
    public interface ITasksService
    {
        public Task<ServiceResponse<IEnumerable<TaskDto>>> GetTasks(Guid userId);
        public Task<ServiceResponse> AddTask(Guid userId, TaskDto taskDto);
    }
}