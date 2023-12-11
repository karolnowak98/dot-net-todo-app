using GlassyCode.ToDo.Abstractions.Exceptions;

namespace GlassyCode.ToDo.Modules.Users.Core.Exceptions;

public class InvalidPasswordException : InflowException
{
    protected internal InvalidPasswordException() : base("Password not match criteria.")
    {
    }
}