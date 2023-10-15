using System.ComponentModel.DataAnnotations;

namespace WebApi.Options;

public sealed record AzureOpenAiOptions
{
    public const string SectionName = "AzureOpenAiConfiguration";

    [Required] public string ApiKey { get; } = string.Empty;

    [Required] public string Endpoint { get; } = string.Empty;

    [Required] public string DeploymentName { get; } = string.Empty;
}

