namespace Application.Services.Interfaces;

public interface IChatService
{
    Task<string?> Ask(string prompt, CancellationToken cancellationToken = default);
}