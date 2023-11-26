using TodoApp.API.Models.Task;
using Task = TodoApp.API.Models.Task.Task;

namespace TodoApp.API.Services.Implementations
{
    public class TasksService : ITasksService
    {
        public TasksService(IMapper mapper, ITasksRepository repository, ICategoryRepository categoryRepository)
        {
            _mapper = mapper;
            _repository = repository;
            _categoryRepository = categoryRepository;
        }
        
        private readonly IMapper _mapper;
        private readonly ITasksRepository _repository;
        private readonly ICategoryRepository _categoryRepository;
        
        public async Task<ServiceResponse<IEnumerable<TaskDto>>> GetTasksForUserAsync(Guid userId)
        {
            var tasks = await _repository.GetAllAsync(userId);
            var taskDtos = new List<TaskDto>();

            foreach (var task in tasks)
            {
                var taskDto = _mapper.Map<TaskDto>(task);
                taskDto.Categories = _categoryRepository.GetAllForTaskAsync(task.Id).Result;
                taskDtos.Add(taskDto);
            }

            return new ServiceResponse<IEnumerable<TaskDto>> { Data = taskDtos };
        }
        
        public async Task<ServiceResponse> CreateTaskAsync(Guid userId, TaskDto taskDto)
        {
            var task = _mapper.Map<Task>(taskDto);
            task.Id = Guid.NewGuid();
            task.UserId = userId;
            
            if (!await _repository.CreateTaskAsync(task))
            {
                return new ServiceResponse
                {
                    Message = "Couldn't create new task!",
                    Success = false
                };
            }

            foreach (var category in taskDto.Categories)
            {
                var categoryId = await _categoryRepository.GetIdByTypeAsync(category.Type);

                if (categoryId == null)
                {
                    continue;
                }
                
                if (!await _repository.CreateTaskCategoryAsync(task.Id, categoryId.Value))
                {
                    return new ServiceResponse
                    {
                        Message = "Couldn't create new task category!",
                        Success = false
                    };
                }
            }
                
            return new ServiceResponse();
        }
    }
}