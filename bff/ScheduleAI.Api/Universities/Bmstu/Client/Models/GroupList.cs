using Newtonsoft.Json;

namespace BmstuSchedule.Client.Models;

public class GroupList
{
    [JsonProperty("items")] public required GroupBase[] Items { get; init; }
    [JsonProperty("total")] public required int Total { get; init; }
    [JsonProperty("page")] public required int Page { get; init; }
    [JsonProperty("size")] public required int Size { get; init; }
}