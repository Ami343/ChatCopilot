namespace Application.Services.Interfaces;

public interface IChatHistoryService
{
    Task<string> GetChatHistoryForBotProcessing(Guid chatSessionId);
}