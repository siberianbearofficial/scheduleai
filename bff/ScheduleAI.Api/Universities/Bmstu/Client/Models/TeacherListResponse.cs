using System.Text.Json.Serialization;

namespace BmstuSchedule.Client.Models;

public class TeacherListResponse
{
    [JsonPropertyName("detail")] public string? Detail { get; init; }
    [JsonPropertyName("data")] public required TeacherList Data { get; init; }
}