using Microsoft.Extensions.Options;
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
                var azureOpenAiOptions = sp.GetRequiredService<IOptions<AzureOpenAiOptions>>().Value;

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