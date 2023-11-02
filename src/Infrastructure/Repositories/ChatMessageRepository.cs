using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Database.Interfaces;
using MongoDB.Driver;

namespace Infrastructure.Repositories;

public class ChatMessageRepository : IChatMessageRepository
{
    private readonly IChatCopilotDbContext _context;

    public ChatMessageRepository(IChatCopilotDbContext context)
    {
        _context = context;
    }

    public Task Create(ChatMessage chatMessage)
        => _context.GetCollection<ChatMessage>().InsertOneAsync(chatMessage);

    public async Task<IEnumerable<ChatMessage>> GetByChatSessionId(string chatSessionId)
    {
        var collection = await _context.GetCollection<ChatMessage>()
            .FindAsync<ChatMessage>(Builders<ChatMessage>.Filter.Eq(x => x.ChatSessionId, chatSessionId));

        return collection.ToEnumerable();
    }
}