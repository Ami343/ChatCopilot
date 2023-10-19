namespace Application.Services.Interfaces;

public interface IChatService
{
    Task<string?> Ask(
        string prompt,
        string pluginName,
        string functionName,
        CancellationToken cancellationToken = default);
}