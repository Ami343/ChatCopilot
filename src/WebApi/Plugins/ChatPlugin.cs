using System.ComponentModel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.AI.ChatCompletion;
using Microsoft.SemanticKernel.Orchestration;
using WebApi.Options;

namespace WebApi.Plugins;

public class ChatPlugin
{
    private readonly IKernel _kernel;

    private readonly PromptOptions _promptOptions;

    public ChatPlugin(
        IKernel kernel,
        PromptOptions promptOptions)
    {
        _kernel = kernel;
        _promptOptions = promptOptions;
    }

    [SKFunction, Description("Process the user input")]
    public async Task<string?> ProcessUserInput(SKContext context, CancellationToken cancellationToken)
    {
        var semanticFunction = _kernel.CreateSemanticFunction(
            promptTemplate:
            "Do not process the input {{$input}}, respond with message saying that you are not available at the moment.",
            functionName: Constants.Constants.ChatPluginName,
            description: "Process the user input.");

        var result = await semanticFunction.InvokeAsync(
            context: context,
            cancellationToken: cancellationToken);

        return result.GetValue<string>();
    }

    [SKFunction, Description("Extract user intent")]
    public async Task<string> ExtractUserIntent(SKContext context, CancellationToken cancellationToken)
    {
        var innerContext = context.Clone();

        var semanticFunction = _kernel.CreateSemanticFunction(
            promptTemplate: _promptOptions.SystemIntentExtraction,
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
        context.Variables.Update(message);

        return message;
    }

    private async Task<string> GetChatResponse(SKContext context, CancellationToken cancellationToken)
    {
        var systemInstructions = await GetSystemInstructions(context, cancellationToken);

        var chatCompletion = _kernel.GetService<IChatCompletion>();
        var promptTemplate = chatCompletion.CreateNewChat(systemInstructions);

        var userIntent = await ExtractUserIntent(context, cancellationToken);
        promptTemplate.AddSystemMessage(userIntent);

        var input = context.Variables["input"];

        promptTemplate.AddUserMessage(input);

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