using System.Collections.Concurrent;
using Domain.Entities;
using Domain.Repositories;

namespace Infrastructure.Repositories.Volatile;

public class ChatMessageVolatileRepository : IChatMessageRepository
{
    private readonly ConcurrentDictionary<Guid, ChatMessage> _entities;

    public ChatMessageVolatileRepository()
    {
        _entities = new ConcurrentDictionary<Guid, ChatMessage>();
    }

    public Task Create(ChatMessage chatMessage)
    {
        _entities.AddOrUpdate(
            chatMessage.Id,
            chatMessage,
            (key, oldValue) => chatMessage);

        return Task.CompletedTask;
    }

    public Task<IEnumerable<ChatMessage>> GetByChatSessionId(Guid chatSessionId)
        => Task.FromResult(_entities.Values
            .Where(x => x.ChatSessionId == chatSessionId)
            .OrderBy(x => x.CreatedOn)
            .Select(x => x));
}