using MediatR;

namespace Application.Chats.Commands.Chat;

public record ChatCommand : IRequest<ChatCommandResponse>
{
    public string Input { get; init; } = string.Empty;
    public Guid ChatSessionId { get; init; }
}