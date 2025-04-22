using System.Text.Json.Serialization;

namespace Bmstu.Mock.Models;

public class Schedule
{
    [JsonPropertyName("uuid")] public Guid? Id { get; set; }
    [JsonPropertyName("title")] public string? Title { get; set; }
    [JsonPropertyName("schedule")] public List<Pair> Data { get; set; } = new();
}