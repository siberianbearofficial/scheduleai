using Newtonsoft.Json;

namespace BmstuSchedule.Client.Models;

public class SchedulePairRead
{
    [JsonProperty("time_slot")] public required TimeSlot TimeSlot { get; init; }
    [JsonProperty("groups")] public required GroupBase[] Groups { get; init; }
    [JsonProperty("disciplines")] public required DisciplineBase[] Disciplines { get; init; }
    [JsonProperty("teachers")] public required TeacherBase[] Teachers { get; init; }
    [JsonProperty("rooms")] public required RoomBase[] Rooms { get; init; }
}