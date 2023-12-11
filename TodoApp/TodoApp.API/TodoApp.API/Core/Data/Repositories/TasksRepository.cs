using TodoApp.API.DTOs.Tasks;
using TodoApp.API.Exceptions;
using TodoApp.API.Interfaces;
using TodoApp.API.Models.TaskCategory;
using Task = TodoApp.API.Models.Task.Task;

namespace TodoApp.API.Core.Data.Repositories;

public class TasksRepository(ApplicationDbContext context) : ITasksRepository
{
    private readonly ApplicationDbContext _context = context ?? throw new DbContextNullException(nameof(context));

    public async Task<IEnumerable<Task>> GetAllAsync(Guid userId)
        => await _context.Tasks.Where(c => c.UserId == userId).ToListAsync();

    public async Task<Task?> GetByIdAsync(Guid taskId)
        => await _context.Tasks.FindAsync(taskId);
        
    public async Task<bool> CreateTaskAsync(Task task)
    {
        _context.Tasks.Add(task);
        return await SaveChangesAsync();
    }

    public async Task<bool> CreateTaskCategoryAsync(Guid taskId, Guid categoryId)
    {
        var taskCategory = new TaskCategory { TaskId = taskId, CategoryId = categoryId };
        _context.TaskCategories.Add(taskCategory);
        return await SaveChangesAsync();
    }

    public async Task<bool> UpdateTaskStatusAsync(Guid userId, UpdateStatusTaskDto updateStatusTaskDto)
    {
        try
        {
            var task = await GetByIdAsync(updateStatusTaskDto.Id);
                
            if (task == null)
            {
                return false;
            }

            if (task.Status != updateStatusTaskDto.Status)
            {
                task.Status = updateStatusTaskDto.Status;
                var changes = await _context.SaveChangesAsync();
                return changes > 0;
            }
                
            return false; 
        }
            
        catch (Exception ex)
        {
            Console.WriteLine($"Couldn't update status, because: { ex.Message }");
            return false;
        }
    }
        
    private async Task<bool> SaveChangesAsync()
    {
        var changes = await _context.SaveChangesAsync();
        return changes > 0;
    }
}