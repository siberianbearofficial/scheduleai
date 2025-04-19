using Newtonsoft.Json;

namespace BmstuSchedule.Client.Models;

public class RoomResponse
{
    [JsonProperty("detail")] public string? Detail { get; init; }
    [JsonProperty("data")] public required RoomBase Data { get; init; }
}