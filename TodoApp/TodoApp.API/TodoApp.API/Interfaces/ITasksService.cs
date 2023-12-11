using TodoApp.API.DTOs;
using TodoApp.API.DTOs.Tasks;

namespace TodoApp.API.Interfaces;

public interface ITasksService
{
    public Task<ServiceResponse<IEnumerable<GetTaskDto>>> GetTasksForUserAsync(Guid userId);
    public Task<ServiceResponse> CreateTaskAsync(Guid userId, GetTaskDto getTaskDto);
    public Task<ServiceResponse> UpdateTaskStatusAsync(Guid userId, UpdateStatusTaskDto updateStatusTaskDto);
}