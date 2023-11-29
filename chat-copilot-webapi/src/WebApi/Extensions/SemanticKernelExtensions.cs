using Application.Constants;
using Application.Options;
using Application.Plugins;
using Application.Services.Interfaces;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Plugins.Core;

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
            functionsInstance: new ChatPlugin(
                kernel,
                sp.GetRequiredService<IOptions<PromptOptions>>().Value,
                sp.GetRequiredService<IChatHistoryService>()),
            pluginName: Constants.ChatPluginName);

        // Built in plugins
        kernel.ImportFunctions(new TimePlugin(), nameof(TimePlugin));

        return kernel;
    }
}