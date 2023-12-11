using GlassyCode.ToDo.Abstractions.Exceptions;

namespace GlassyCode.ToDo.Modules.Users.Core.Exceptions;

internal class UserNotFoundException : InflowException
{
    public string Email { get; }
    public Guid UserId { get; }

    public UserNotFoundException(Guid userId) : base($"User with Id: '{userId}' was not found.")
    {
        UserId = userId;
    }

    public UserNotFoundException(string email) : base($"User with Id: '{email}' was not found.")
    {
        Email = email;
    }
}