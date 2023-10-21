using Domain.Entities;

namespace Domain.Repositories;

public interface IChatRepository
{
    Task Create(ChatMessage chatMessage);

    Task<IEnumerable<ChatMessage>> GetByChatSessionId(Guid chatSessionId);
}