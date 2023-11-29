using MongoDB.Driver;

namespace Infrastructure.Database.Interfaces;

public interface IChatCopilotDbContext
{
    IMongoDatabase Database { get; }

    IMongoCollection<T> GetCollection<T>() where T : class;
}