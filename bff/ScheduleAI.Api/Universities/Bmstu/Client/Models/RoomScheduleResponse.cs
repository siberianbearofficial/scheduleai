using System.Text.Json.Serialization;

namespace BmstuSchedule.Client.Models;

public class RoomScheduleResponse
{
    [JsonPropertyName("detail")] public string? Detail { get; init; }
    [JsonPropertyName("data")] public required RoomSchedule Data { get; init; }
}