using Newtonsoft.Json;

namespace BmstuSchedule.Client.Models;

public class RoomScheduleResponse
{
    [JsonProperty("detail")] public string? Detail { get; init; }
    [JsonProperty("data")] public required RoomSchedule Data { get; init; }
}