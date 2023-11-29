using Application.Chats.Models;

namespace Application.Chats.Queries.GetByChatSessionId;

public record GetByChatSessionIdQueryResponse(IEnumerable<ChatMessageDto> Messages);