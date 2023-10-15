using WebApi.Options;

namespace WebApi.Extensions;

public static class AppOptionExtensions
{
    public static IServiceCollection AddAppOptions(this IServiceCollection services)
    {
        AddOption<AzureOpenAiOptions>(services, AzureOpenAiOptions.SectionName);

        return services;
    }

    private static void AddOption<TOption>(
        IServiceCollection services,
        string sectionName)
        where TOption : class
    {
        services.AddOptions<TOption>()
            .BindConfiguration(sectionName)
            .ValidateDataAnnotations()
            .ValidateOnStart();
    }
}