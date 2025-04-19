using Newtonsoft.Json;

namespace BmstuSchedule.Client.Models;

public class RoomListResponse
{
    [JsonProperty("detail")] public string? Detail { get; init; }
    [JsonProperty("data")] public required RoomList Data { get; init; }
}