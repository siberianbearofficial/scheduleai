using System.Text.Json.Serialization;

namespace BmstuSchedule.Client.Models;

public class RoomResponse
{
    [JsonPropertyName("detail")] public string? Detail { get; init; }
    [JsonPropertyName("data")] public required RoomBase Data { get; init; }
}