using Application.ChatSessions.Models;
using Domain.Entities;
using Domain.Repositories;
using MediatR;

namespace Application.ChatSessions.Queries.GetChatSessions;

public class GetChatSessionsQueryHandler : IRequestHandler<GetChatSessionsQueryParams, GetChatSessionsQueryResponse>
{
    private readonly IChatSessionRepository _chatSessionRepository;

    public GetChatSessionsQueryHandler(IChatSessionRepository chatSessionRepository)
    {
        _chatSessionRepository = chatSessionRepository;
    }

    public async Task<GetChatSessionsQueryResponse> Handle(GetChatSessionsQueryParams request,
        CancellationToken cancellationToken)
    {
        var chatSessions = await _chatSessionRepository.GetByUserId(request.UserId);

        var chatSessionDTOs = Map(chatSessions);

        return new GetChatSessionsQueryResponse(chatSessionDTOs);
    }

    private static IEnumerable<ChatSessionDto> Map(IEnumerable<ChatSession> sessions)
        => sessions.Select(x => new ChatSessionDto(x.Id, x.CreatedOn));
}