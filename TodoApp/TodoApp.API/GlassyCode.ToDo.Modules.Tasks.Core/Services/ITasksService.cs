using GlassyCode.ToDo.Modules.Tasks.Core.DTOs.Task;

namespace GlassyCode.ToDo.Modules.Tasks.Core.Services;

public interface ITasksService
{
    public Task<ServiceResponse<IEnumerable<GetTaskDto>>> GetTasksForUserAsync(Guid userId);
    public Task<ServiceResponse> CreateTaskAsync(Guid userId, GetTaskDto getTaskDto);
    public Task<ServiceResponse> UpdateTaskStatusAsync(UpdateStatusTaskDto updateStatusTaskDto);
    public Task<ServiceResponse<GetTaskDto>> EditTaskAsync(GetTaskDto getTaskDto);
}