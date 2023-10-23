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
    [Required] public string SystemIntentContinuation { get; init; } = string.Empty;
    public int PromptTimeoutInSeconds { get; init; } = 15;

    public string BotPersona => $"{SystemDescription}\n{SystemResponse}";

    public string GetSystemIntentExtraction(string chatHistoryText)
        => $"{SystemDescription}\n" +
           $"{SystemIntent}\n" +
           $"{chatHistoryText}\n" +
           $"{SystemIntentContinuation}";
}