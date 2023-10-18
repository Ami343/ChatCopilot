using System.ComponentModel.DataAnnotations;

namespace Application.Models.Request;

public record AskRequest
{
    [Required] public string Input { get; init; } = string.Empty;
}