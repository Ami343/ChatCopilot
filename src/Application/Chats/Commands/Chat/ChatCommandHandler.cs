using Application.Services.Interfaces;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Chats.Commands.Chat;

public class ChatCommandHandler : IRequestHandler<ChatCommand, ChatCommandResponse>
{
    private readonly IChatService _chatService;
    private readonly IChatMessageRepository _chatMessageRepository;

    public ChatCommandHandler(
        IChatService chatService,
        IChatMessageRepository chatMessageRepository)
    {
        _chatService = chatService;
        _chatMessageRepository = chatMessageRepository;
    }

    public async Task<ChatCommandResponse> Handle(ChatCommand request, CancellationToken cancellationToken)
    {
        var result = await _chatService.Ask(
            prompt: request.Input,
            pluginName: Constants.Constants.ChatPluginName,
            functionName: Constants.Constants.ChatPluginChatFunction,
            cancellationToken);

        var userMessage = ChatMessage.CreateUserMessage(request.ChatSessionId, request.Input);
        var botResponse = ChatMessage.CreateBotMessage(request.ChatSessionId, result);

        await _chatMessageRepository.Create(userMessage);
        await _chatMessageRepository.Create(botResponse);

        return new ChatCommandResponse(result!);
    }
}