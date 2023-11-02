using MediatR;

namespace Application.Chats.Queries.GetByChatSessionId;

public class GetByChatSessionIdQueryParams : IRequest<GetByChatSessionIdQueryResponse>
{
    public string ChatSessionId { get; init; }
}