using Application.Services.Interfaces;
using MediatR;

namespace Application.Chats.Commands.Chat;

public class ChatCommandHandler : IRequestHandler<ChatRequest, ChatCommandResponse>
{
    private readonly IChatService _chatService;

    public ChatCommandHandler(IChatService chatService)
    {
        _chatService = chatService;
    }

    public async Task<ChatCommandResponse> Handle(ChatRequest request, CancellationToken cancellationToken)
    {
        var result = await _chatService.Ask(
            prompt: request.Input,
            pluginName: Constants.Constants.ChatPluginName,
            functionName: Constants.Constants.ChatPluginChatFunction,
            cancellationToken);

        return new ChatCommandResponse(result!);
    }
}