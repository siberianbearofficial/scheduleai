using System.Text.Json.Serialization;

namespace BmstuSchedule.Client.Models;

public class TeacherList
{
    [JsonPropertyName("items")] public required TeacherBase[] Items { get; init; }
    [JsonPropertyName("total")] public required int Total { get; init; }
    [JsonPropertyName("page")] public required int? Page { get; init; }
    [JsonPropertyName("size")] public required int? Size { get; init; }
}