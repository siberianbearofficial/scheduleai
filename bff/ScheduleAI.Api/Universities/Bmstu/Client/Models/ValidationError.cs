using System.Text.Json.Serialization;

namespace BmstuSchedule.Client.Models;

public class ValidationError
{
    [JsonPropertyName("loc")] public required string?[] Loc { get; init; }
    [JsonPropertyName("msg")] public required string Msg { get; init; }
    [JsonPropertyName("type")] public required string Type { get; init; }
}