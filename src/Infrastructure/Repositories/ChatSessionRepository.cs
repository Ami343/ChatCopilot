using Domain.Entities;
using Domain.Primitives;
using Domain.Repositories;
using Infrastructure.Database.Interfaces;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class ChatSessionRepository : IChatSessionRepository
{
    private readonly IChatCopilotDbContext _context;

    public ChatSessionRepository(IChatCopilotDbContext context)
    {
        _context = context;
    }

    public Task Create(ChatSession chatSession)
        => _context.GetCollection<ChatSession>().InsertOneAsync(chatSession);

    public async Task<Maybe<ChatSession>> GetById(string id)
    {
        var collection = await _context.GetCollection<ChatSession>()
            .FindAsync<ChatSession>(Builders<ChatSession>.Filter.Eq(x => x.Id, id));

        return Maybe<ChatSession>.From(collection.FirstOrDefault());
    }
}