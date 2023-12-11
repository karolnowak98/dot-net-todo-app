using GlassyCode.ToDo.Abstractions.Events;

namespace GlassyCode.ToDo.Modules.Users.Core.Events;

internal record SignedIn(Guid UserId) : IEvent;