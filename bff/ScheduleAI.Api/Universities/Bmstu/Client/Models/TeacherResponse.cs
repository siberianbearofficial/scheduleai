using System.Text.Json.Serialization;

namespace BmstuSchedule.Client.Models;

public class TeacherResponse
{
    [JsonPropertyName("detail")] public string? Detail { get; init; }
    [JsonPropertyName("data")] public required TeacherBase Data { get; init; }
}