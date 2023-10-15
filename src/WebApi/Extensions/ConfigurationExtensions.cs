namespace WebApi.Extensions;

public static class ConfigurationExtensions
{
    public static IHostBuilder AddConfiguration(this IHostBuilder host)
    {
        var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        host.ConfigureAppConfiguration((_, configurationBuilder) =>
        {
            configurationBuilder.AddJsonFile(
                path: "appsettings.json",
                optional: false,
                reloadOnChange: true);

            configurationBuilder.AddJsonFile(
                path: $"appsettings.{environment}.json",
                optional: true,
                reloadOnChange: true);

            configurationBuilder.AddEnvironmentVariables();
        });

        return host;
    }
}