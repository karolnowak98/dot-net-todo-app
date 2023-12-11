using GlassyCode.ToDo.Abstractions.Contexts;

namespace GlassyCode.ToDo.Abstractions.Messaging;

public interface IMessageContext
{
    public Guid MessageId { get; }
    public IContext Context { get; }
}