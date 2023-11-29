using System.Collections.Concurrent;
using Domain.Entities;
using Domain.Primitives;
using Domain.Repositories;

namespace Infrastructure.Repositories.Volatile;

public class ChatSessionVolatileRepository : IChatSessionRepository
{
    private readonly ConcurrentDictionary<string, ChatSession> _entities;
    
    public ChatSessionVolatileRepository()
    {
        _entities = new ConcurrentDictionary<string, ChatSession>();
    }
    
    public Task Create(ChatSession chatSession)
    {
        _entities.AddOrUpdate(
            chatSession.Id,
            chatSession,
            (key, oldValue) => chatSession);

        return Task.CompletedTask;
    }

    public Task<Maybe<ChatSession>> GetById(string id)
    {
        var result = Maybe<ChatSession>.From(
            _entities.Values.FirstOrDefault(x => x.Id == id));
     
        return Task.FromResult(result);
    }

    public Task<IEnumerable<ChatSession>> GetByUserId(string userId)
    {
        var entities = _entities.Values;
        //TODO filter by userId after auth introduction 

        return Task.FromResult(entities.AsEnumerable());
    }
}