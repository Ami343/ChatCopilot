using System.Globalization;
using Ardalis.GuardClauses;
using Domain.Attributes;
using Domain.Enums;
using Domain.Primitives;

namespace Domain.Entities;

[MongoCollection("ChatMessages")]
public class ChatMessage : MongoEntity
{
    public string ChatSessionId { get; private set; }
    public string Content { get; private set; }
    public MessageActor Actor { get; private set; }
    public DateTimeOffset CreatedOn { get; private set; }
    public string UserName { get; private set; }

    private ChatMessage(string chatSessionId, string content, MessageActor actor, string userName)
        : base(id: Guid.NewGuid())
    {
        Guard.Against.NullOrEmpty(chatSessionId);

        ChatSessionId = chatSessionId;
        Content = content;
        Actor = actor;
        CreatedOn = DateTimeOffset.Now;
        UserName = userName;
    }

    public static ChatMessage CreateBotMessage(string chatSessionId, string content)
        => new(
            chatSessionId: chatSessionId,
            content: content,
            actor: MessageActor.Bot,
            userName: MessageActor.Bot.ToString());

    public static ChatMessage CreateUserMessage(string chatSessionId, string content)
        => new(
            chatSessionId: chatSessionId,
            content: content,
            actor: MessageActor.User,
            userName: MessageActor.User.ToString());

    public string ToFormattedMessage()
    {
        var prefix = $"[{CreatedOn.ToString("G", CultureInfo.CurrentCulture)}]";

        return $"{prefix} {UserName} said: {Content}";
    }
}