using MediatR;

namespace Application.Chats.Queries.GetByChatSessionId;

public class GetByChatSessionIdQueryParams : IRequest<GetByChatSessionIdQueryResponse>
{
    public Guid ChatSessionId { get; init; }
}