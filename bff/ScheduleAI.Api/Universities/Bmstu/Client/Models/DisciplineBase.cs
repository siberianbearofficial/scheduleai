using Newtonsoft.Json;

namespace BmstuSchedule.Client.Models;

public class DisciplineBase
{
    [JsonProperty("id")] public required int Id { get; init; }
    [JsonProperty("full_name")] public required string FullName { get; init; }
    [JsonProperty("short_name")] public required string ShortName { get; init; }
    [JsonProperty("act_type")] public string? ActType { get; init; }
}