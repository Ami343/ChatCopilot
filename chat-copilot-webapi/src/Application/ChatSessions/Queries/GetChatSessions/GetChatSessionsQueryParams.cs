using MediatR;

namespace Application.ChatSessions.Queries.GetChatSessions;

public class GetChatSessionsQueryParams : IRequest<GetChatSessionsQueryResponse>
{
    public string UserId { get; init; } = string.Empty;
}