using Ardalis.GuardClauses;
using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities;

public class ChatMessage : Entity
{
    public Guid ChatSessionId { get; private set; }
    public string Content { get; private set; }
    public MessageActor Actor { get; private set; }
    public DateTimeOffset CreatedOn { get; private set; }

    private ChatMessage(Guid chatSessionId, string content, MessageActor actor)
        : base(id: Guid.NewGuid())
    {
        Guard.Against.NullOrEmpty(chatSessionId);
        
        ChatSessionId = chatSessionId;
        Content = content;
        Actor = actor;
        CreatedOn = DateTimeOffset.Now;
    }

    public static ChatMessage CreateBotMessage(Guid chatSessionId, string content)
        => new(
            chatSessionId: chatSessionId,
            content: content,
            actor: MessageActor.Bot);
    
    public static ChatMessage CreateUserMessage(Guid chatSessionId, string content)
        => new(
            chatSessionId: chatSessionId,
            content: content,
            actor: MessageActor.User);
}