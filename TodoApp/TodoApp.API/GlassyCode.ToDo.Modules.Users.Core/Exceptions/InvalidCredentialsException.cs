using GlassyCode.ToDo.Abstractions.Exceptions;

namespace GlassyCode.ToDo.Modules.Users.Core.Exceptions;

public class InvalidCredentialsException : InflowException
{
    protected internal InvalidCredentialsException() : base("Invalid credentials.")
    {
    }
}