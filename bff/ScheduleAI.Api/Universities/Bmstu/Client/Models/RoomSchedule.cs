using Newtonsoft.Json;

namespace BmstuSchedule.Client.Models;

public class RoomSchedule
{
    [JsonProperty("room")] public required RoomBase Room { get; init; }
    [JsonProperty("schedule")] public required SchedulePairRead[] Schedule { get; init; }
}