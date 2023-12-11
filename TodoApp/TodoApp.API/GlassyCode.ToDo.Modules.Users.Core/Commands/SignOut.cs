using GlassyCode.ToDo.Abstractions.Commands;

namespace GlassyCode.ToDo.Modules.Users.Core.Commands;

public record SignOut(Guid UserId) : ICommand;