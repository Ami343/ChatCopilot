using System.ComponentModel.DataAnnotations;

namespace Application.Chats.Commands.Chat;

public class ChatRequest
{
    [Required] public string Input { get; set; } = string.Empty;

    public ChatCommand GetCommand(Guid chatSessionId)
        => new ChatCommand
        {
            Input = Input,
            ChatSessionId = chatSessionId
        };
}