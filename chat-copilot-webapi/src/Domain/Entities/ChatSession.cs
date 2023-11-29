using Domain.Attributes;
using Domain.Primitives;

namespace Domain.Entities;

[MongoCollection("ChatSessions")]
public class ChatSession : MongoEntity
{
    public DateTimeOffset CreatedOn { get; private set; }

    public ChatSession()
        : base(id: Guid.NewGuid())
    {
        CreatedOn = DateTimeOffset.Now;
    }
}