using GlassyCode.ToDo.Modules.Tasks.Core.DTOs.Task;
using GlassyCode.ToDo.Modules.Tasks.Core.Entities;
using Task = GlassyCode.ToDo.Modules.Tasks.Core.Entities.Task;

namespace GlassyCode.ToDo.Modules.Tasks.Core.DAL.Repositories;

public class TasksRepository : ITasksRepository
{
    public TasksRepository(ApplicationDbContext context)
    {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }
        
    private readonly ApplicationDbContext _context;

    public async Task<IEnumerable<Task>> GetAllAsync(Guid userId)
    {
            return await _context.Tasks.Where(c => c.UserId == userId).ToListAsync();
        }

    public async Task<Task?> GetByIdAsync(Guid taskId)
    {
            return await _context.Tasks.FindAsync(taskId);
        }
        
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

    public async Task<bool> UpdateTaskStatusAsync(UpdateStatusTaskDto updateStatusTaskDto)
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

    public async Task<GetTaskDto?> EditTaskAsync(GetTaskDto getTaskDto)
    {
            try
            {
                var task = await GetByIdAsync(getTaskDto.Id);

                if (task == null)
                {
                    return null;
                }

                task.Status = getTaskDto.Status;
                task.Deadline = getTaskDto.Deadline;
                task.Priority = getTaskDto.Priority;
                task.Description = getTaskDto.Description;
                task.Title = getTaskDto.Title;
                
                await SaveChangesAsync();
            }
            
            catch(Exception ex)
            {
                Console.WriteLine($"Couldn't update status, because: { ex.Message }");
                return null;
            }

            return null;
        }
        
    private async Task<bool> SaveChangesAsync()
    {
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }
}