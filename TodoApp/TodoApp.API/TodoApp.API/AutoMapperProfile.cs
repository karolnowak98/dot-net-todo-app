using TodoApp.API.Core.Models;
using TodoApp.API.Models.Category;
using TodoApp.API.Models.Task;
using TodoApp.API.Models.User.Dto;
using Task = TodoApp.API.Models.Task.Task;

namespace TodoApp.API
{
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
}