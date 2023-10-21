using Infrastructure.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
    {

        return services;
    }

    private static void AddMongoDb(IServiceCollection services)
    {
        services.AddSingleton<IMongoClient>(sp =>
        {
            var dbConfig = sp.GetRequiredService<IOptions<DatabaseOptions>>().Value;
            return new MongoClient(dbConfig.ConnectionString);
        });
    }
}