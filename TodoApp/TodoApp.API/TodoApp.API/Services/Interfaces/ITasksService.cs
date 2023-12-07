using TodoApp.API.Models.Task;

namespace TodoApp.API.Services.Interfaces
{
    public interface ITasksService
    {
        public Task<ServiceResponse<IEnumerable<GetTaskDto>>> GetTasksForUserAsync(Guid userId);
        public Task<ServiceResponse> CreateTaskAsync(Guid userId, GetTaskDto getTaskDto);
        public Task<ServiceResponse> UpdateTaskStatusAsync(Guid userId, UpdateStatusTaskDto updateStatusTaskDto);
    }
}