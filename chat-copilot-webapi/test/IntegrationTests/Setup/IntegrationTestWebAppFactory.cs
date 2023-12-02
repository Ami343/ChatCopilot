using Infrastructure.Options;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Testcontainers.MongoDb;

namespace IntegrationTests.Setup;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private const string Environment = "IntegrationTests";

    private readonly MongoDbContainer _mongoDbContainer = new MongoDbBuilder()
        .WithName("chat-copilot-integration-tests")
        .WithImage("mongo:latest")
        .WithUsername("ChatDb")
        .WithPassword("pass")
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        System.Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", Environment);

        builder.ConfigureTestServices(services =>
        {
            var mongoDbDescriptor = services.SingleOrDefault(x => x.ServiceType == typeof(IMongoClient));

            if (mongoDbDescriptor is not null)
                services.Remove(mongoDbDescriptor);

            services.AddSingleton<IMongoClient>(new MongoClient(_mongoDbContainer.GetConnectionString()));

            AddMockedDbOptions(services, _mongoDbContainer.GetConnectionString(), "chat-copilot-tests");
        });
    }

    private static void AddMockedDbOptions(
        IServiceCollection services,
        string connectionString,
        string databaseName)
    {
        var dbOptionsDescriptor = services.SingleOrDefault(x => x.ServiceType == typeof(IOptions<DatabaseOptions>));

        if (dbOptionsDescriptor is not null)
            services.Remove(dbOptionsDescriptor);

        var dbOptions = new DatabaseOptions
        {
            ConnectionString = connectionString,
            DatabaseName = databaseName
        };

        services.AddSingleton(Options.Create(dbOptions));
    }

    public Task InitializeAsync()
        => _mongoDbContainer.StartAsync();

    public Task DisposeAsync()
        => _mongoDbContainer.DisposeAsync().AsTask();
}