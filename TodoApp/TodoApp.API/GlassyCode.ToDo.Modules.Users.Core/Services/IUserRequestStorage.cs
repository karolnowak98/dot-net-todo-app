using GlassyCode.ToDo.Abstractions.Auth;

namespace GlassyCode.ToDo.Modules.Users.Core.Services;

public interface IUserRequestStorage
{
    void SetToken(Guid commandId, JsonWebToken jwt);
    JsonWebToken GetToken(Guid commandId);
}