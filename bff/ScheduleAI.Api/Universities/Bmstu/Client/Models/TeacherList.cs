using Newtonsoft.Json;

namespace BmstuSchedule.Client.Models;

public class TeacherList
{
    [JsonProperty("items")] public required TeacherBase[] Items { get; init; }
    [JsonProperty("total")] public required int Total { get; init; }
    [JsonProperty("page")] public required int Page { get; init; }
    [JsonProperty("size")] public required int Size { get; init; }
}