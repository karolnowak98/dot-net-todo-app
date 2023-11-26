using Task = TodoApp.API.Models.Task.Task;

namespace TodoApp.API.Repositories.Interfaces
{
    public interface ITasksRepository
    {
        public Task<IEnumerable<Task>> GetAllAsync(Guid userId);
        public Task<bool> CreateTaskAsync(Task task);
        public Task<bool> CreateTaskCategoryAsync(Guid taskId, Guid categoryId);
    }
}