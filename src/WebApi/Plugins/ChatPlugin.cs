using System.ComponentModel;
using Microsoft.SemanticKernel;
using Microsoft.SemanticKernel.Orchestration;

namespace WebApi.Plugins;

public class ChatPlugin
{
    private readonly IKernel _kernel;

    public ChatPlugin(IKernel kernel)
    {
        _kernel = kernel;
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
}