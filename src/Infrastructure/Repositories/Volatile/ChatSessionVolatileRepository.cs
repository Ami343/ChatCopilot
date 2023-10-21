using System.Collections.Concurrent;
using Domain.Entities;
using Domain.Primitives;
using Domain.Repositories;

namespace Infrastructure.Repositories.Volatile;

public class ChatSessionVolatileRepository : IChatSessionRepository
{
    public static ChatSessionVolatileRepository Instance { get; } = new();
    private readonly ConcurrentDictionary<Guid, ChatSession> _entities;
    
    public ChatSessionVolatileRepository()
    {
        _entities = new ConcurrentDictionary<Guid, ChatSession>();
    }
    
    public Task Create(ChatSession chatSession)
    {
        _entities.AddOrUpdate(
            chatSession.Id,
            chatSession,
            (key, oldValue) => chatSession);

        return Task.CompletedTask;
    }

    public Task<Maybe<ChatSession>> GetById(Guid id)
    {
        var result = Maybe<ChatSession>.From(
            _entities.Values.FirstOrDefault(x => x.Id == id));
     
        return Task.FromResult(result);
    }
}