namespace Application.Services.Interfaces;

public interface IChatHistoryService
{
    Task<string> GetChatHistoryForBotProcessing(string chatSessionId);
}