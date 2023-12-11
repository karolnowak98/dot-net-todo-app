using GlassyCode.ToDo.Modules.Tasks.Core.DTOs.Task;
using Task = GlassyCode.ToDo.Modules.Tasks.Core.Entities.Task;

namespace GlassyCode.ToDo.Modules.Tasks.Core.Repositories;

public interface ITasksRepository
{
    public Task<IEnumerable<Task>> GetAllAsync(Guid userId);
    public Task<Task?> GetByIdAsync(Guid taskId);
    public Task<bool> CreateTaskAsync(Task task);
    public Task<bool> CreateTaskCategoryAsync(Guid taskId, Guid categoryId);
    public Task<bool> UpdateTaskStatusAsync(UpdateStatusTaskDto updateStatusTaskDto);
    public Task<GetTaskDto?> EditTaskAsync(GetTaskDto getTaskDto);
}