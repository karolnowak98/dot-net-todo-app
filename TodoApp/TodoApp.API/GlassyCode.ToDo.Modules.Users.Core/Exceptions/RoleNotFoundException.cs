using GlassyCode.ToDo.Abstractions.Exceptions;

namespace GlassyCode.ToDo.Modules.Users.Core.Exceptions;

public class RoleNotFoundException : InflowException
{
    protected internal RoleNotFoundException(string roleName) : base($"Role '{roleName}' not found.")
    {
    }
}