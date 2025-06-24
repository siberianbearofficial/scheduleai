using System.Text.Json.Serialization;

namespace BmstuSchedule.Client.Models;

public class GroupResponse
{
    [JsonPropertyName("detail")] public string? Detail { get; init; }
    [JsonPropertyName("data")] public required GroupBase Data { get; init; }
}