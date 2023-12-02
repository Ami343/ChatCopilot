using MediatR;

namespace Application.ChatSessions.Commands.Create;

public class CreateChatSessionRequest : IRequest<CreateChatSessionCommandResponse>
{
    // public string Name { get; set; } = "Test";
}