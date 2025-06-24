using System.Text.Json.Serialization;

namespace BmstuSchedule.Client.Models;

public class TeacherScheduleResponse
{
    [JsonPropertyName("detail")] public string? Detail { get; init; }
    [JsonPropertyName("data")] public required TeacherSchedule Data { get; init; }
}