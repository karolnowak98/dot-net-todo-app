using TodoApp.API.DTOs;
using TodoApp.API.DTOs.Tasks;
using TodoApp.API.Interfaces;
using Task = TodoApp.API.Models.Task.Task;

namespace TodoApp.API.Services;

public class TasksService(IMapper mapper, ITasksRepository tasksRepo, 
    ICategoryRepository categoryRepo) : ITasksService
{
    public async Task<ServiceResponse<IEnumerable<GetTaskDto>>> GetTasksForUserAsync(Guid userId)
    {
        var tasks = await tasksRepo.GetAllAsync(userId);
        var taskDtos = new List<GetTaskDto>();

        foreach (var task in tasks)
        {
            var taskDto = mapper.Map<GetTaskDto>(task);
            taskDto.Categories = await categoryRepo.GetAllForTaskAsync(task.Id);
            taskDtos.Add(taskDto);
        }

        return new ServiceResponse<IEnumerable<GetTaskDto>> { Data = taskDtos };
    }

    private async Task<GetTaskDto> GetTaskByIdAsync(Guid taskId)
    {
        var task = await tasksRepo.GetByIdAsync(taskId);
        var getTaskDto = mapper.Map<GetTaskDto>(task);
            
        getTaskDto.Categories = await categoryRepo.GetAllForTaskAsync(taskId);

        return getTaskDto;
    }
        
    public async Task<ServiceResponse> CreateTaskAsync(Guid userId, GetTaskDto getTaskDto)
    {
        var task = mapper.Map<Task>(getTaskDto);
            
        task.Id = Guid.NewGuid();
        task.UserId = userId;
            
        if (!await tasksRepo.CreateTaskAsync(task))
        {
            return new ServiceResponse
            {
                Message = "Couldn't create new task!",
                Success = false
            };
        }

        foreach (var category in getTaskDto.Categories)
        {
            var categoryId = await categoryRepo.GetIdByTypeAsync(category.Type);

            if (categoryId == null)
            {
                continue;
            }
                
            if (!await tasksRepo.CreateTaskCategoryAsync(task.Id, categoryId.Value))
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
        var result = await tasksRepo.UpdateTaskStatusAsync(userId, updateStatusTaskDto);

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