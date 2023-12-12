using TodoApp.API.Core.Data.Identity;
using TodoApp.API.DTOs.Tasks;
using TodoApp.API.DTOs.Users;
using TodoApp.API.Models.Category;
using Task = TodoApp.API.Models.Task.Task;

namespace TodoApp.API.Core.Other;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<ApplicationUser, GetUserDto>();
        CreateMap<GetUserDto, ApplicationUser>();
        CreateMap<RegisterDto, ApplicationUser>();
            
        CreateMap<Task, GetTaskDto>();
        CreateMap<GetTaskDto, Task>();
            
        CreateMap<CategoryDto, Category>();
        CreateMap<Category, CategoryDto>();
    }
}