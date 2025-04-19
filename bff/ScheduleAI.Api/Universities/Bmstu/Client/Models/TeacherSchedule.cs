using Newtonsoft.Json;

namespace BmstuSchedule.Client.Models;

public class TeacherSchedule
{
    [JsonProperty("teacher")] public required TeacherBase Teacher { get; init; }
    [JsonProperty("schedule")] public required SchedulePairRead[] Schedule { get; init; }
}