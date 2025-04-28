using System.Text.Json.Serialization;

namespace ScheduleAI.Core.Models;

public class Pair
{
    [JsonPropertyName("teachers")] public string[] Teachers { get; init; } = [];
    [JsonPropertyName("groups")] public required string[] Groups { get; init; }
    [JsonPropertyName("startTime")] public required DateTime StartTime { get; init; }
    [JsonPropertyName("endTime")] public required DateTime EndTime { get; init; }
    [JsonPropertyName("rooms")] public string[] Rooms { get; init; } = [];
    [JsonPropertyName("discipline")] public string? Discipline { get; init; }
    [JsonPropertyName("actType")] public string? ActType { get; init; }
    [JsonPropertyName("convenience")] public PairConvenience? Convenience { get; init; }
}