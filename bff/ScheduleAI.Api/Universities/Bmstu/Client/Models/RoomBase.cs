using Newtonsoft.Json;

namespace BmstuSchedule.Client.Models;

public class RoomBase
{
    [JsonProperty("id")] public required int Id { get; init; }
    [JsonProperty("name")] public required string Name { get; init; }
    [JsonProperty("building")] public string? Building { get; init; }
    [JsonProperty("map_url")] public string? MapUrl { get; init; }
}