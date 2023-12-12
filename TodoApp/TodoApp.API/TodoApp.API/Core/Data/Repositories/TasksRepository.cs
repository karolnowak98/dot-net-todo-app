using MediatR;
using TodoApp.API.DTOs.Tasks;
using TodoApp.API.Interfaces;
using TodoApp.API.UseCases.Tasks.Commands;
using TodoApp.API.UseCases.Tasks.Queries;
using Task = TodoApp.API.Models.Task.Task;

namespace TodoApp.API.Core.Data.Repositories;

public class TasksRepository(ISender sender) : ITasksRepository
{
    public async Task<IEnumerable<Task>> GetAllAsync(Guid userId)
        => await sender.Send(new GetAllTasksForUserQuery(userId));

    public async Task<Task?> GetByIdAsync(Guid taskId)
        => await sender.Send(new GetTaskByIdQuery(taskId));

    public async Task<bool> CreateTaskAsync(Task task)
        => await sender.Send(new CreateTaskCommand(task));

    public async Task<bool> CreateTaskCategoryAsync(Guid taskId, Guid categoryId)
        => await sender.Send(new CreateTaskCategoryCommand(taskId, categoryId));

    public async Task<bool> UpdateTaskStatusAsync(Guid userId, UpdateStatusTaskDto updateStatusTaskDto)
        => await sender.Send(new UpdateTaskStatusCommand(userId, updateStatusTaskDto));        
}