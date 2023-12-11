using GlassyCode.ToDo.Modules.Users.Core.DTOs;
using GlassyCode.ToDo.Modules.Users.Core.Entities;

namespace GlassyCode.ToDo.Modules.Users.Core.Queries.Handlers;

internal static class Extensions
{
    public static UserDto AsDto(this User member)
        => member.Map<UserDto>();

    public static UserDetailsDto AsDetailsDto(this User user)
    {
        var dto = user.Map<UserDetailsDto>();
        dto.Permissions = user.Role.Permissions;

        return dto;
    }

    private static T Map<T>(this User user) where T : UserDto, new()
        => new()
        {
            UserId = user.Id,
            Email = user.Email,
            Role = user.Role.Name,
            CreatedAt = user.CreatedAt
        };
}