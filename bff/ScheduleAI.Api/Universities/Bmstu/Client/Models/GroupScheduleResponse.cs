using System.Text.Json.Serialization;

namespace BmstuSchedule.Client.Models;

public class GroupScheduleResponse
{
    [JsonPropertyName("detail")] public string? Detail { get; init; }
    [JsonPropertyName("data")] public required GroupSchedule Data { get; init; }
}