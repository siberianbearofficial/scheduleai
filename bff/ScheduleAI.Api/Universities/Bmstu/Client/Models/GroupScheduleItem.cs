using Newtonsoft.Json;

namespace BmstuSchedule.Client.Models;

public class GroupScheduleItem
{
    [JsonConverter(typeof(DayOfWeek))][JsonProperty("day")] public required DayOfWeek Day { get; init; }
    [JsonConverter(typeof(Week))][JsonProperty("week")] public required Week Week { get; init; }
    [JsonProperty("time_slot")] public required TimeSlot TimeSlot { get; init; }
    [JsonProperty("teachers")] public TeacherBase[]? Teachers { get; init; }
    [JsonProperty("discipline")] public required DisciplineBase Discipline { get; init; }
    [JsonProperty("room")] public required RoomBase Room { get; init; }
}