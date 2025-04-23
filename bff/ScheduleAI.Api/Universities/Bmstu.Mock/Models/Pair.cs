using System.Text.Json.Serialization;

namespace Bmstu.Mock.Models;

public class Pair
{
    [JsonPropertyName("groups")] public List<Group> Groups { get; set; } = new();
    [JsonPropertyName("audiences")] public List<Audience> Audiences { get; set; } = new();
    [JsonPropertyName("teachers")] public List<Teacher> Teachers { get; set; } = new();
    [JsonPropertyName("discipline")] public Discipline? Discipline { get; set; }
    [JsonPropertyName("day")] public int? Day { get; set; }
    [JsonPropertyName("time")] public int? Time { get; set; }
    [JsonPropertyName("week")] public string? Week { get; set; }
    [JsonPropertyName("startTime")] public string? StartTime { get; set; }
    [JsonPropertyName("endTime")] public string? EndTime { get; set; }
}