using System.Text.Json.Serialization;

namespace BmstuSchedule.Client.Models;

public class GroupBase
{
    [JsonPropertyName("id")] public required int Id { get; init; }
    [JsonPropertyName("abbr")] public required string Abbr { get; init; }
    [JsonPropertyName("course_id")] public required int? CourseId { get; init; }
    [JsonPropertyName("semester_num")] public required int SemesterNum { get; init; }
}