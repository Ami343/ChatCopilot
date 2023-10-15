using System.ComponentModel.DataAnnotations;

namespace WebApi.Models.Request;

public record AskRequest
{
    [Required] public string Input { get; init; } = string.Empty;
}