using System.Text.Json.Serialization;

namespace BmstuSchedule.Client.Models;

public class DisciplineBase
{
    [JsonPropertyName("id")] public required int Id { get; init; }
    [JsonPropertyName("abbr")] public required string Abbr { get; init; }
    [JsonPropertyName("full_name")] public required string FullName { get; init; }
    [JsonPropertyName("short_name")] public required string ShortName { get; init; }
    [JsonPropertyName("act_type")] public string? ActType { get; init; }
}