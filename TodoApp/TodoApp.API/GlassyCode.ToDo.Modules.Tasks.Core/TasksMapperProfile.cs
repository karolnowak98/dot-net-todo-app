using AutoMapper;
using GlassyCode.ToDo.Modules.Tasks.Core.DTOs;
using GlassyCode.ToDo.Modules.Tasks.Core.DTOs.Task;
using GlassyCode.ToDo.Modules.Tasks.Core.Entities;
using Task = GlassyCode.ToDo.Modules.Tasks.Core.Entities.Task;

namespace GlassyCode.ToDo.Modules.Tasks.Core;

public class TasksMapperProfile : Profile
{
    public TasksMapperProfile()
    {
        CreateMap<Task, GetTaskDto>();
        CreateMap<GetTaskDto, Task>();
            
        CreateMap<CategoryDto, Category>();
        CreateMap<Category, CategoryDto>();
    }
}