using Application.Options;
using Domain.Entities;
using Domain.Repositories;
using MediatR;
using Microsoft.Extensions.Options;

namespace Application.ChatSessions.Commands.Create;

public class CreateChatSessionCommandHandler
    : IRequestHandler<CreateChatSessionRequest, CreateChatSessionCommandResponse>
{
    private readonly IChatSessionRepository _chatSessionRepository;
    private readonly IChatMessageRepository _chatMessageRepository;
    private readonly PromptOptions _promptOptions;

    public CreateChatSessionCommandHandler(
        IChatSessionRepository chatSessionRepository,
        IChatMessageRepository chatMessageRepository,
        IOptions<PromptOptions> promptOptions)
    {
        _chatSessionRepository = chatSessionRepository;
        _chatMessageRepository = chatMessageRepository;
        _promptOptions = promptOptions.Value;
    }

    public async Task<CreateChatSessionCommandResponse> Handle(
        CreateChatSessionRequest request,
        CancellationToken cancellationToken)
    {
        var chatSession = new ChatSession();
        await _chatSessionRepository.Create(chatSession);

        var chatMessage = ChatMessage.CreateBotMessage(chatSession.Id, _promptOptions.InitialBotMessage);
        await _chatMessageRepository.Create(chatMessage);

        return new CreateChatSessionCommandResponse(chatSession.Id, chatMessage.Content);
    }
}