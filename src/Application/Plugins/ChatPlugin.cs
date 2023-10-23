using System.ComponentModel;
using Application.Options;
using Application.Services.Interfaces;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.AI.ChatCompletion;
using Microsoft.SemanticKernel.Orchestration;

namespace Application.Plugins;

public class ChatPlugin
{
    private readonly IKernel _kernel;

    private readonly PromptOptions _promptOptions;

    private readonly IChatHistoryService _chatHistoryService;

    public ChatPlugin(
        IKernel kernel,
        PromptOptions promptOptions,
        IChatHistoryService chatHistoryService)
    {
        _kernel = kernel;
        _promptOptions = promptOptions;
        _chatHistoryService = chatHistoryService;
    }

    [SKFunction, Description("Extract chat history")]
    public Task<string> ExtractChatHistory(
        SKContext context,
        CancellationToken cancellationToken = default)
    {
        var chatSessionId = Guid.Parse(context.Variables["chatSessionId"]);

        return _chatHistoryService.GetChatHistoryForBotProcessing(chatSessionId);
    }

    [SKFunction, Description("Extract user intent")]
    public async Task<string> ExtractUserIntent(SKContext context, CancellationToken cancellationToken)
    {
        var innerContext = context.Clone();

        var chatHistoryText = await ExtractChatHistory(innerContext, cancellationToken);

        var semanticFunction = _kernel.CreateSemanticFunction(
            promptTemplate: _promptOptions.GetSystemIntentExtraction(chatHistoryText),
            functionName: Constants.Constants.ChatPluginName,
            description: "Complete the prompt.");

        var result = await semanticFunction.InvokeAsync(
            context: innerContext,
            cancellationToken: cancellationToken);

        return $"User intent: {result.GetValue<string>()}";
    }

    [SKFunction, Description("Get chat response")]
    public async Task<string> Chat(SKContext context, CancellationToken cancellationToken)
    {
        var chatContext = context.Clone();
        SetKnowledgeCutOffDateVariable(chatContext);

        var message = await GetChatResponse(chatContext, cancellationToken);

        return message;
    }

    private async Task<string> GetChatResponse(SKContext context, CancellationToken cancellationToken)
    {
        var systemInstructions = await GetSystemInstructions(context, cancellationToken);

        var chatCompletion = _kernel.GetService<IChatCompletion>();
        var promptTemplate = chatCompletion.CreateNewChat(systemInstructions);

        var userIntent = await ExtractUserIntent(context, cancellationToken);
        promptTemplate.AddUserMessage(userIntent);
       
        var result =
            await chatCompletion.GenerateMessageAsync(promptTemplate, default, cancellationToken);

        return result;
    }

    private Task<string> GetSystemInstructions(SKContext context, CancellationToken cancellationToken)
        => _kernel.PromptTemplateEngine.RenderAsync(
            _promptOptions.BotPersona,
            context,
            cancellationToken);

    private void SetKnowledgeCutOffDateVariable(SKContext context)
    {
        context.Variables.Set(Constants.Constants.KnowledgeCutOffDate, _promptOptions.KnowledgeCutOffDate);
    }
}