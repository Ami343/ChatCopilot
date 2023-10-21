using Domain.Primitives;

namespace Domain.Entities;

public class ChatSession : Entity
{
    public DateTimeOffset CreatedOn { get; private set; }

    public ChatSession()
        : base(id: Guid.NewGuid())
    {
        CreatedOn = DateTimeOffset.Now;
    }
}