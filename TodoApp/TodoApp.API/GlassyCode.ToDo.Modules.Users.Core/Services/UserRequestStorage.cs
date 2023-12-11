using GlassyCode.ToDo.Abstractions.Auth;
using GlassyCode.ToDo.Abstractions.Storage;

namespace GlassyCode.ToDo.Modules.Users.Core.Services;

internal class UserRequestStorage(IRequestStorage requestStorage) : IUserRequestStorage
{
    public void SetToken(Guid commandId, JsonWebToken jwt) => requestStorage.Set(GetKey(commandId), jwt);

    public JsonWebToken GetToken(Guid commandId) => requestStorage.Get<JsonWebToken>(GetKey(commandId));

    private static string GetKey(Guid commandId) => $"jwt:{commandId:N}";
}