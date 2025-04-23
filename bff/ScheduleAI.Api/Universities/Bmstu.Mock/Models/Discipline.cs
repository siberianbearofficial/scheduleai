using System.Text.Json.Serialization;

namespace Bmstu.Mock.Models;

public class Discipline
{
    [JsonPropertyName("abbr")] public string? Abbr { get; set; }
    [JsonPropertyName("actType")] public string? ActType { get; set; }
    [JsonPropertyName("fullName")] public string? FullName { get; set; }
    [JsonPropertyName("shortName")] public string? ShortName { get; set; }
}