using TodoApp.API.Core.Models;
using TodoApp.API.Models.User.Dto;

namespace TodoApp.API
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<ApplicationUser, GetUserDto>();
            CreateMap<GetUserDto, ApplicationUser>();
            CreateMap<RegisterDto, ApplicationUser>();
        }
    }
}