using GlassyCode.ToDo.Abstractions.Exceptions;

namespace GlassyCode.ToDo.Modules.Users.Core.Exceptions;

internal class SignUpDisabledException : InflowException
{
    protected internal SignUpDisabledException() : base("Sign up is disabled.")
    {
    }
}