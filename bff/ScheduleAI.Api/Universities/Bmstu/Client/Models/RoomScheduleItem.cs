using Newtonsoft.Json;

namespace BmstuSchedule.Client.Models;

public class RoomScheduleItem
{
    [JsonConverter(typeof(DayOfWeek))][JsonProperty("day")] public required DayOfWeek Day { get; init; }
    [JsonProperty("time_slot")] public required TimeSlot TimeSlot { get; init; }
    [JsonProperty("groups")] public required GroupBase[] Groups { get; init; }
    [JsonProperty("teachers")] public required TeacherBase[] Teachers { get; init; }
    [JsonProperty("disciplines")] public required DisciplineBase[] Disciplines { get; init; }
}