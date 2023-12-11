using GlassyCode.ToDo.Abstractions.Queries;
using GlassyCode.ToDo.Modules.Users.Core.DTOs;

namespace GlassyCode.ToDo.Modules.Users.Core.Queries;

public class GetUserByEmail : IQuery<UserDetailsDto>
{
    public string Email { get; set; }
}