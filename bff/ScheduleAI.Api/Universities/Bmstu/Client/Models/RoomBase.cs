using System.Text.Json.Serialization;

namespace BmstuSchedule.Client.Models;

public class RoomBase
{
    [JsonPropertyName("id")] public required int Id { get; init; }
    [JsonPropertyName("name")] public required string Name { get; init; }
    [JsonPropertyName("building")] public string? Building { get; init; }
    [JsonPropertyName("map_url")] public string? MapUrl { get; init; }
}