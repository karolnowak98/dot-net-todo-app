using TodoApp.API.Models.Task;
using Task = TodoApp.API.Models.Task.Task;

namespace TodoApp.API.Services.Implementations
{
    public class TasksService : ITasksService
    {
        public TasksService(IMapper mapper, ITasksRepository tasksRepo, ICategoryRepository categoryRepo)
        {
            _mapper = mapper;
            _tasksRepo = tasksRepo;
            _categoryRepo = categoryRepo;
        }
        
        private readonly IMapper _mapper;
        private readonly ITasksRepository _tasksRepo;
        private readonly ICategoryRepository _categoryRepo;
        
        public async Task<ServiceResponse<IEnumerable<GetTaskDto>>> GetTasksForUserAsync(Guid userId)
        {
            var tasks = await _tasksRepo.GetAllAsync(userId);
            var taskDtos = new List<GetTaskDto>();

            foreach (var task in tasks)
            {
                var taskDto = _mapper.Map<GetTaskDto>(task);
                taskDto.Categories = await _categoryRepo.GetAllForTaskAsync(task.Id);
                taskDtos.Add(taskDto);
            }

            return new ServiceResponse<IEnumerable<GetTaskDto>> { Data = taskDtos };
        }

        private async Task<GetTaskDto> GetTaskByIdAsync(Guid taskId)
        {
            var task = await _tasksRepo.GetByIdAsync(taskId);
            var getTaskDto = _mapper.Map<GetTaskDto>(task);
            
            getTaskDto.Categories = await _categoryRepo.GetAllForTaskAsync(taskId);

            return getTaskDto;
        }
        
        public async Task<ServiceResponse> CreateTaskAsync(Guid userId, GetTaskDto getTaskDto)
        {
            var task = _mapper.Map<Task>(getTaskDto);
            
            task.Id = Guid.NewGuid();
            task.UserId = userId;
            
            if (!await _tasksRepo.CreateTaskAsync(task))
            {
                return new ServiceResponse
                {
                    Message = "Couldn't create new task!",
                    Success = false
                };
            }

            foreach (var category in getTaskDto.Categories)
            {
                var categoryId = await _categoryRepo.GetIdByTypeAsync(category.Type);

                if (categoryId == null)
                {
                    continue;
                }
                
                if (!await _tasksRepo.CreateTaskCategoryAsync(task.Id, categoryId.Value))
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

        public async Task<ServiceResponse> UpdateTaskStatusAsync(Guid userId, UpdateStatusTaskDto updateStatusTaskDto)
        {
            var result = await _tasksRepo.UpdateTaskStatusAsync(userId, updateStatusTaskDto);

            if (!result)
            {
                return new ServiceResponse()
                {
                    Success = false,
                    Message = "Couldn't update status!"
                };
            }
            
            return new ServiceResponse();
        }
    }
}