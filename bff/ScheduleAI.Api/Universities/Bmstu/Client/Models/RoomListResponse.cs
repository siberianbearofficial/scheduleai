using System.Text.Json.Serialization;

namespace BmstuSchedule.Client.Models;

public class RoomListResponse
{
    [JsonPropertyName("detail")] public string? Detail { get; init; }
    [JsonPropertyName("data")] public required RoomList Data { get; init; }
}