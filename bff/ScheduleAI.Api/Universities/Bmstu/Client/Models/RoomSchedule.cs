using System.Text.Json.Serialization;

namespace BmstuSchedule.Client.Models;

public class RoomSchedule
{
    [JsonPropertyName("room")] public required RoomBase Room { get; init; }
    [JsonPropertyName("schedule")] public required SchedulePairRead[] Schedule { get; init; }
}