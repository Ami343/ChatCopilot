using System.ComponentModel.DataAnnotations;

namespace WebApi.Options;

public sealed record PromptOptions
{
    public const string SectionName = "Prompt";

    [Required] public string KnowledgeCutOffDate { get; init; } = string.Empty;
    [Required] public string SystemDescription { get; init; } = string.Empty;
    [Required] public string SystemResponse { get; init; } = string.Empty;
}