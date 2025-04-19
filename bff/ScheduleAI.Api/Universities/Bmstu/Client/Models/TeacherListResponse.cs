using Newtonsoft.Json;

namespace BmstuSchedule.Client.Models;

public class TeacherListResponse
{
    [JsonProperty("detail")] public string? Detail { get; init; }
    [JsonProperty("data")] public required TeacherList Data { get; init; }
}