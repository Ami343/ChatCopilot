using WebApi.Models.Request;
using WebApi.Models.Response;

namespace WebApi.Services.Interfaces;

public interface IChatService
{
    Task<AskResponse?> Ask(AskRequest request, CancellationToken cancellationToken = default);
}