using Microsoft.SemanticKernel;
using WebApi.Options;

namespace WebApi.Extensions;

public static class SemanticKernelExtensions
{
    public static IServiceCollection AddSemanticKernel(this IServiceCollection services)
    {
        services
            .AddScoped<IKernel>((sp) =>
            {
                var azureOpenAiOptions = sp.GetRequiredService<AzureOpenAiOptions>();

                var kernel = new KernelBuilder()
                    .WithAzureChatCompletionService(
                        azureOpenAiOptions.DeploymentName,
                        azureOpenAiOptions.Endpoint,
                        azureOpenAiOptions.ApiKey)
                    .WithLoggerFactory(sp.GetRequiredService<ILoggerFactory>())
                    .Build();

                return kernel;
            });

        return services;
    }
}