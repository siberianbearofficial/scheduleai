using System.Text.Json.Serialization;

namespace ScheduleAI.Core.Models;

public class PairConvenience
{
    [JsonPropertyName("coefficient")] public required double Coefficient { get; init; }
    [JsonPropertyName("collisions")] public Pair[] Collisions { get; init; } = [];
    [JsonPropertyName("waitTime")] public TimeSpan? WaitTime { get; init; }
    [JsonPropertyName("status")] public MergedPairStatus Status { get; init; }
}