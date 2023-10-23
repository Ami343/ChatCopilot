using System.Text;
using Application.Services.Interfaces;
using Domain.Repositories;

namespace Application.Services;

public class ChatHistoryService : IChatHistoryService
{
    private readonly IChatMessageRepository _chatMessageRepository;

    public ChatHistoryService(IChatMessageRepository chatMessageRepository)
    {
        _chatMessageRepository = chatMessageRepository;
    }

    public async Task<string> GetChatHistoryForBotProcessing(Guid chatSessionId)
    {
        var historyMessages = (await _chatMessageRepository.GetByChatSessionId(chatSessionId)).ToList();

        var sb = new StringBuilder();

        foreach (var formattedMessage in historyMessages.Select(message => message.ToFormattedMessage()))
        {
            sb.AppendLine(formattedMessage);
        }

        var historyText = sb.ToString().Trim();

        return $"Chat history:\n{historyText}";
    }
}