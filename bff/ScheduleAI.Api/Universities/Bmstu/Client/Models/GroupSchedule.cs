using Newtonsoft.Json;

namespace BmstuSchedule.Client.Models;

public class GroupSchedule
{
    [JsonProperty("group")] public required GroupBase Group { get; init; }
    [JsonProperty("schedule")] public required SchedulePairRead[] Schedule { get; init; }
}