using TodoApp.API.Models.Task;
using Task = TodoApp.API.Models.Task.Task;

namespace TodoApp.API.Repositories.Implementations
{
    public class TasksRepository : ITasksRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public TasksRepository(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TaskDto>> GetTasks(Guid userId)
        {
            var tasks = await _context.Tasks.Where(t => t.UserId == userId).ToListAsync();
            return _mapper.Map<IEnumerable<TaskDto>>(tasks);
        }
        
        public async Task<bool> AddTask(Guid userId, TaskDto taskDto)
        {
            var task = _mapper.Map<Task>(taskDto);
            task.UserId = userId;
            await _context.Tasks.AddAsync(task);
    
            var changes = await _context.SaveChangesAsync();

            return changes > 0;
        }
    }
}