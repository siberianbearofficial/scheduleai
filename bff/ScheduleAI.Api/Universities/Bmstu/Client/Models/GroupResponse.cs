using Newtonsoft.Json;

namespace BmstuSchedule.Client.Models;

public class GroupResponse
{
    [JsonProperty("detail")] public string? Detail { get; init; }
    [JsonProperty("data")] public required GroupBase Data { get; init; }
}