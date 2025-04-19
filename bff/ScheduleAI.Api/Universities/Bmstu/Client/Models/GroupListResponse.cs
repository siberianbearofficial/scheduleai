using Newtonsoft.Json;

namespace BmstuSchedule.Client.Models;

public class GroupListResponse
{
    [JsonProperty("detail")] public string? Detail { get; init; }
    [JsonProperty("data")] public required GroupList Data { get; init; }
}