using Application.Chats.Models;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.Chats.Queries.GetByChatSessionId;

public class GetByChatSessionIdQueryHandler : IRequestHandler<GetByChatSessionIdQueryParams, GetByChatSessionIdQueryResponse>
{
    private readonly IChatMessageRepository _chatMessageRepository;

    public GetByChatSessionIdQueryHandler(IChatMessageRepository chatMessageRepository)
    {
        _chatMessageRepository = chatMessageRepository;
    }

    public async Task<GetByChatSessionIdQueryResponse> Handle(
        GetByChatSessionIdQueryParams request,
        CancellationToken cancellationToken)
    {
        var messages = await _chatMessageRepository.GetByChatSessionId(request.ChatSessionId);

        return new GetByChatSessionIdQueryResponse(Map(messages));
    }

    private static IEnumerable<ChatMessageDto> Map(IEnumerable<ChatMessage> chatMessages)
    {
        return chatMessages.Select(x => new ChatMessageDto(
            Content: x.Content,
            Actor: x.Actor,
            CreatedOn: x.CreatedOn));
    }
}