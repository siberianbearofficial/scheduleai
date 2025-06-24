using System.Text.Json.Serialization;

namespace BmstuSchedule.Client.Models;

public class GroupListResponse
{
    [JsonPropertyName("detail")] public string? Detail { get; init; }
    [JsonPropertyName("data")] public required GroupList Data { get; init; }
}