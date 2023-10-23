using Application.Options;
using Application.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Diagnostics;
using Microsoft.SemanticKernel.Orchestration;

namespace Application.Services;

public class ChatService : IChatService
{
    private readonly IKernel _kernel;
    private readonly ILogger<ChatService> _logger;
    private readonly PromptOptions _promptOptions;

    public ChatService(
        IKernel kernel,
        ILogger<ChatService> logger,
        IOptions<PromptOptions> promptOptions)
    {
        _kernel = kernel;
        _logger = logger;
        _promptOptions = promptOptions.Value;
    }

    public async Task<string?> GetBotResponse(
        string prompt,
        Guid chatSessionId,
        CancellationToken cancellationToken = default)
    {
        var function = GetFunction();

        cancellationToken.ThrowIfCancellationRequested();

        var ctxVariables = GetContextVariables(input: prompt, chatSessionId: chatSessionId);

        var result = await ExecuteFunction(function, ctxVariables);

        cancellationToken.ThrowIfCancellationRequested();

        return result?.GetValue<string>();
    }

    private ISKFunction GetFunction()
    {
        try
        {
            return _kernel.Functions.GetFunction(
                pluginName: Constants.Constants.ChatPluginName,
                functionName: Constants.Constants.ChatPluginChatFunction);
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

    private async Task<KernelResult> ExecuteFunction(
        ISKFunction function,
        ContextVariables contextVariables)
    {
        try
        {
            using var cts = GetCancellationTokenSource();

            return await _kernel.RunAsync(function, contextVariables, cts.Token);
        }
        catch (Exception e)
        {
            _logger.LogError("Failed to execute function, error: {exception}", e);
            throw;
        }
    }

    private static ContextVariables GetContextVariables(
        string input,
        Guid chatSessionId)
    {
        var contextVariables = new ContextVariables(input);
        contextVariables.Set("chatSessionId", chatSessionId.ToString());

        return contextVariables;
    }

    private CancellationTokenSource GetCancellationTokenSource()
        => new(TimeSpan.FromSeconds(_promptOptions.PromptTimeoutInSeconds));
}