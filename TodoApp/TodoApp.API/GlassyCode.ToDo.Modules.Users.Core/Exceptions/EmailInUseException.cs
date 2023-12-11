using GlassyCode.ToDo.Abstractions.Exceptions;

namespace GlassyCode.ToDo.Modules.Users.Core.Exceptions;

public class EmailInUseException : InflowException
{
    protected internal EmailInUseException(string email) : base($"Email '{email}' is already in use.")
    {
    }
}