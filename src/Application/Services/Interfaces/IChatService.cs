namespace Application.Services.Interfaces;

public interface IChatService
{
    Task<string?> GetBotResponse(
        string prompt,
        string chatSessionId,
        CancellationToken cancellationToken = default);
}