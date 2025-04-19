using Newtonsoft.Json;

namespace BmstuSchedule.Client.Models;

public class TeacherScheduleResponse
{
    [JsonProperty("detail")] public string? Detail { get; init; }
    [JsonProperty("data")] public required TeacherSchedule Data { get; init; }
}