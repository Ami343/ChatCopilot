using Domain.Attributes;
using Infrastructure.Database.Interfaces;
using Infrastructure.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure.Database;

public class ChatCopilotDbContext : IChatCopilotDbContext
{
    public IMongoDatabase Database { get; }

    public ChatCopilotDbContext(IMongoClient client, IOptions<DatabaseOptions> databaseOptions)
    {
        var dbConfig = databaseOptions.Value;
        Database = client.GetDatabase(dbConfig.DatabaseName);
    }

    public IMongoCollection<T> GetCollection<T>() where T : class
    {
        return Database.GetCollection<T>(GetCollectionName<T>());
    }

    private static string GetCollectionName<T>() where T : class
        => (typeof(T).GetCustomAttributes(typeof(MongoCollectionAttribute), true)
               .FirstOrDefault() as MongoCollectionAttribute)
           ?.CollectionName
           ?? throw new InvalidOperationException("No collection name was defined");
}