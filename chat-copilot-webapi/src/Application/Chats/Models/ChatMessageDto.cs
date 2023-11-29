using Domain.Enums;

namespace Application.Chats.Models;

public record ChatMessageDto(string Content, MessageActor Actor, DateTimeOffset CreatedOn);