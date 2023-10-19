using Application.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Diagnostics;
using Microsoft.SemanticKernel.Orchestration;

namespace Application.Services;

public class ChatService : IChatService
{
    private readonly IKernel _kernel;
    private readonly ILogger<ChatService> _logger;

    public ChatService(IKernel kernel, ILogger<ChatService> logger)
    {
        _kernel = kernel;
        _logger = logger;
    }

    public async Task<string?> Ask(
        string prompt,
        string pluginName,
        string functionName,
        CancellationToken cancellationToken = default)
    {
        var function = GetFunction(functionName, pluginName);

        var result = await ExecuteFunction(prompt, function);

        return result?.GetValue<string>();
    }

    private ISKFunction GetFunction(string functionName, string pluginName)
    {
        try
        {
            return _kernel.Functions.GetFunction(
                pluginName: pluginName,
                functionName: functionName);
        }
        catch (SKException e)
        {
            _logger.LogError("Failed to get function: {pluginName}/{functionName}, error: {exception}",
                Constants.Constants.ChatPluginName,
                Constants.Constants.ChatPluginChatFunction,
                e);

            throw;
        }
    }

    private async Task<KernelResult> ExecuteFunction(string input, ISKFunction function)
    {
        try
        {
            using var cts = GetCancellationTokenSource();

            var ctxVariables = GetContextVariables(input);

            return await _kernel.RunAsync(function, ctxVariables, cts.Token);
        }
        catch (Exception e)
        {
            _logger.LogError("Failed to execute function, error: {exception}", e);
            throw;
        }
    }

    private static ContextVariables GetContextVariables(string input)
    {
        var contextVariables = new ContextVariables();
        contextVariables.Set("input", input);

        return contextVariables;
    }

    private static CancellationTokenSource GetCancellationTokenSource() => new(TimeSpan.FromSeconds(15));
}