using System.ComponentModel.DataAnnotations;

namespace Infrastructure.Options;

public class DatabaseOptions
{
    public const string SectionName = "Database";

    [Required] public string ConnectionString { get; init; } = string.Empty;

    [Required] public string DatabaseName { get; init; } = string.Empty;
}