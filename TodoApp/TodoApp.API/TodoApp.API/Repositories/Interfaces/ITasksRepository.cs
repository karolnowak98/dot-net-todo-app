using TodoApp.API.Models.Task;

namespace TodoApp.API.Repositories.Interfaces
{
    public interface ITasksRepository
    {
        public Task<IEnumerable<TaskDto>> GetTasks(Guid userId);
        Task<bool> AddTask(Guid userId, TaskDto taskDto);
    }
}