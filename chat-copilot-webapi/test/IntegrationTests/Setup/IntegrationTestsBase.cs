using Infrastructure.Database.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace IntegrationTests.Setup;

public abstract class IntegrationTestsBase : IClassFixture<IntegrationTestWebAppFactory>, IDisposable
{
    private readonly IServiceScope _scope;
    protected readonly IChatCopilotDbContext DbContext;
    protected readonly HttpClientDecorator HttpClient;

    protected IntegrationTestsBase(IntegrationTestWebAppFactory factory)
    {
        _scope = factory.Services.CreateScope();

        DbContext = _scope.ServiceProvider.GetRequiredService<IChatCopilotDbContext>();
        var httpClient = factory.CreateClient();
        HttpClient = new HttpClientDecorator(httpClient);
    }

    protected T GetService<T>() where T : notnull
        => _scope.ServiceProvider.GetRequiredService<T>();

    public void Dispose()
    {
        _scope?.Dispose();
    }
}