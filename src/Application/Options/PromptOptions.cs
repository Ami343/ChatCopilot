using System.ComponentModel.DataAnnotations;

namespace Application.Options;

public sealed record PromptOptions
{
    public const string SectionName = "Prompt";

    [Required] public string KnowledgeCutOffDate { get; init; } = string.Empty;
    [Required] public string SystemDescription { get; init; } = string.Empty;
    [Required] public string SystemResponse { get; init; } = string.Empty;
    [Required] public string SystemIntent { get; init; } = string.Empty;
    [Required] public string InitialBotMessage { get; init; } = string.Empty;

    public string BotPersona => $"{SystemDescription}\n{SystemResponse}";
    public string SystemIntentExtraction => $"{SystemDescription}\n{SystemIntent}\n{{input}}";
}