namespace Application.Services.Interfaces;

public interface IChatService
{
    Task<string?> GetBotResponse(
        string prompt,
        Guid chatSessionId,
        CancellationToken cancellationToken = default);
}