using Application.ChatSessions.Models;

namespace Application.ChatSessions.Queries.GetChatSessions;

public record GetChatSessionsQueryResponse(IEnumerable<ChatSessionDto> ChatSessions);