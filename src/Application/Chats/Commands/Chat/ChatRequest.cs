using System.ComponentModel.DataAnnotations;
using MediatR;

namespace Application.Chats.Commands.Chat;

public record ChatRequest : IRequest<ChatCommandResponse>
{
    [Required] public string Input { get; init; } = string.Empty;
}