using TodoApp.API.Models.TaskCategory;
using Task = TodoApp.API.Models.Task.Task;

namespace TodoApp.API.Repositories.Implementations
{
    public class TasksRepository : ITasksRepository
    {
        public TasksRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        
        private readonly ApplicationDbContext _context;

        public async Task<IEnumerable<Task>> GetAllAsync(Guid userId)
        {
            return await _context.Tasks.Where(c => c.UserId == userId).ToListAsync();
        }
        
        public async Task<bool> CreateTaskAsync(Task task)
        {
            await _context.Tasks.AddAsync(task);
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> CreateTaskCategoryAsync(Guid taskId, Guid categoryId)
        {
            var taskCategory = new TaskCategory { TaskId = taskId, CategoryId = categoryId };
            await _context.TaskCategories.AddAsync(taskCategory);
            var changes = await _context.SaveChangesAsync();
            return changes > 0;
        }
    }
}