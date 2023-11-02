using Domain.Repositories;
using Infrastructure.Database;
using Infrastructure.Database.Interfaces;
using Infrastructure.Options;
using Infrastructure.Repositories;
using Infrastructure.Repositories.Volatile;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        if (configuration.GetValue<bool>("UseInMemoryStorage"))
        {
            services.AddSingleton<IChatMessageRepository, ChatMessageVolatileRepository>();
            services.AddSingleton<IChatSessionRepository, ChatSessionVolatileRepository>();
        }
        else
        {
            AddMongoDb(services);

            services.AddSingleton<IChatMessageRepository, ChatMessageRepository>();
            services.AddSingleton<IChatSessionRepository, ChatSessionRepository>();
        }


        return services;
    }

    private static void AddMongoDb(IServiceCollection services)
    {
        services.AddSingleton<IMongoClient>(sp =>
        {
            var dbConfig = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            return new MongoClient(dbConfig.ConnectionString);
        });

        services.AddSingleton<IChatCopilotDbContext, ChatCopilotDbContext>();
    }
}