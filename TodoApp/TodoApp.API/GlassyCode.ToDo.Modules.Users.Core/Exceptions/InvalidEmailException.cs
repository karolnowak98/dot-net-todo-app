using GlassyCode.ToDo.Abstractions.Exceptions;

namespace GlassyCode.ToDo.Modules.Users.Core.Exceptions;

public class InvalidEmailException : InflowException
{
    protected internal InvalidEmailException(string email) : base($"Email")
    {
    }
}