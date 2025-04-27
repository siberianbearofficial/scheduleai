using System.Text.Json.Serialization;

namespace ScheduleAI.Core.Models;

public enum MergedPairStatus
{
    [JsonPropertyName("beforePairs")] BeforePairs,
    [JsonPropertyName("afterPairs")] AfterPairs,
    [JsonPropertyName("inGap")] InGap,
    [JsonPropertyName("collision")] Collision,
    [JsonPropertyName("this")] This,
    [JsonPropertyName("noPairs")] NoPairs,
}