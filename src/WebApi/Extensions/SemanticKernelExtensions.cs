using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Plugins.Core;
using WebApi.Options;
using WebApi.Plugins;

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

                kernel.RegisterSemanticKernelPlugins(sp);

                return kernel;
            });

        return services;
    }

    private static IKernel RegisterSemanticKernelPlugins(this IKernel kernel, IServiceProvider sp)
    {
        // Custom plugins
        kernel.ImportFunctions(
            new ChatPlugin(kernel, sp.GetRequiredService<IOptions<PromptOptions>>().Value),
            Constants.Constants.ChatPluginName);

        // Built in plugins
        kernel.ImportFunctions(new TimePlugin(), nameof(TimePlugin));

        return kernel;
    }
}