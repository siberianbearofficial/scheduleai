using Newtonsoft.Json;

namespace BmstuSchedule.Client.Models;

public class TeacherResponse
{
    [JsonProperty("detail")] public string? Detail { get; init; }
    [JsonProperty("data")] public required TeacherBase Data { get; init; }
}