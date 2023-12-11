namespace GlassyCode.ToDo.Modules.Users.Core.DTOs;

public class UserDetailsDto : UserDto
{
    public IEnumerable<string> Permissions { get; set; }
}