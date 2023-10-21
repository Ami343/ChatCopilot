using Domain.Entities;

namespace Domain.Repositories;

public interface IChatMessageRepository
{
    Task Create(ChatMessage chatMessage);

    Task<IEnumerable<ChatMessage>> GetByChatSessionId(Guid chatSessionId);
}