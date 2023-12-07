using TodoApp.API.Models.Task;
using TodoApp.API.Models.TaskCategory;
using Task = TodoApp.API.Models.Task.Task;
using TaskStatus = TodoApp.API.Models.Task.Enums.TaskStatus;

namespace TodoApp.API.Repositories.Implementations
{
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
}