using System.Text.Json.Serialization;

namespace BmstuSchedule.Client.Models;

public class GroupSchedule
{
    [JsonPropertyName("group")] public required GroupBase Group { get; init; }
    [JsonPropertyName("schedule")] public required SchedulePairRead[] Schedule { get; init; }
}