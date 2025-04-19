using Newtonsoft.Json;

namespace BmstuSchedule.Client.Models;

public class GroupBase
{
    [JsonProperty("id")] public required int Id { get; init; }
    [JsonProperty("abbr")] public required string Abbr { get; init; }
    [JsonProperty("course_id")] public required int CourseId { get; init; }
    [JsonProperty("semester_num")] public required int SemesterNum { get; init; }
}