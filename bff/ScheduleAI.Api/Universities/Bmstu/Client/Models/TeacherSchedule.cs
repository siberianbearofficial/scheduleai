using System.Text.Json.Serialization;

namespace BmstuSchedule.Client.Models;

public class TeacherSchedule
{
    [JsonPropertyName("teacher")] public required TeacherBase Teacher { get; init; }
    [JsonPropertyName("schedule")] public required SchedulePairRead[] Schedule { get; init; }
}