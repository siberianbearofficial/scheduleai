using Newtonsoft.Json;

namespace BmstuSchedule.Client.Models;

public class GroupScheduleResponse
{
    [JsonProperty("detail")] public string? Detail { get; init; }
    [JsonProperty("data")] public required GroupSchedule Data { get; init; }
}