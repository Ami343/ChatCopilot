using Microsoft.SemanticKernel;
using WebApi.Models.Request;
using WebApi.Models.Response;
using WebApi.Services.Interfaces;

namespace WebApi.Services;

public class ChatService : IChatService
{
    private readonly IKernel _kernel;

    public ChatService(IKernel kernel)
    {
        _kernel = kernel;
    }

    public async Task<AskResponse> Ask(AskRequest request)
    {
        var skFunction = _kernel.CreateSemanticFunction(
            "Do not process the input {{$input}}, respond with message saying that you are not available at the moment.");

        var result = await _kernel.RunAsync(request.Input, skFunction);

        return new AskResponse(result.GetValue<string>());
    }
}