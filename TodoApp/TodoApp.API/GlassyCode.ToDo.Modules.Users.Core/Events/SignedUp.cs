using GlassyCode.ToDo.Abstractions.Events;

namespace GlassyCode.ToDo.Modules.Users.Core.Events;

internal record SignedUp(Guid UserId, string Email, string Role) : IEvent;