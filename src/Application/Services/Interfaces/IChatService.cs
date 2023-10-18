using Application.Models.Request;
using Application.Models.Response;

namespace Application.Services.Interfaces;

public interface IChatService
{
    Task<AskResponse?> Ask(AskRequest request, CancellationToken cancellationToken = default);
}