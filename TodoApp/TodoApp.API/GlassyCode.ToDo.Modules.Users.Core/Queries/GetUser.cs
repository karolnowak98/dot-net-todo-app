using GlassyCode.ToDo.Abstractions.Queries;
using GlassyCode.ToDo.Modules.Users.Core.DTOs;

namespace GlassyCode.ToDo.Modules.Users.Core.Queries;

public class GetUser : IQuery<UserDetailsDto>
{
    public Guid UserId { get; set; }
}