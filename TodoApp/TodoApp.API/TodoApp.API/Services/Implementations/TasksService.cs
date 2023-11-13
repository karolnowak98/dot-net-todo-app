using TodoApp.API.Models.Task;

namespace TodoApp.API.Services.Implementations
{
    public class TasksService : ITasksService
    {
        private readonly ITasksRepository _repository;
        
        public TasksService(ITasksRepository repository)
        {
            _repository = repository;
        }
        
        public async Task<ServiceResponse<IEnumerable<TaskDto>>> GetTasks(Guid userId)
        {
            var tasks = await _repository.GetTasks(userId);
            
            return new ServiceResponse<IEnumerable<TaskDto>> { Data = tasks };
        }

        public async Task<ServiceResponse> AddTask(Guid userId, TaskDto taskDto)
        {
            var completed = await _repository.AddTask(userId, taskDto);

            if (!completed)
            {
                return new ServiceResponse
                {
                    Message = "Couldn't create new task!",
                    Success = false
                };
            }
                
            return new ServiceResponse();
        }
    }
}