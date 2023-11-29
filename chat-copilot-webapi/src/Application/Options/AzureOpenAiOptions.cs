using System.ComponentModel.DataAnnotations;

namespace Application.Options;

public sealed record AzureOpenAiOptions
{
    public const string SectionName = "AzureOpenAi";

    [Required] public string ApiKey { get; init; } = string.Empty;

    [Required] public string Endpoint { get; init; } = string.Empty;

    [Required] public string DeploymentName { get; init; } = string.Empty;
}

