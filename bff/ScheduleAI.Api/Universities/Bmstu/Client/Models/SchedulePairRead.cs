using System.Text.Json.Serialization;

namespace BmstuSchedule.Client.Models;

public class SchedulePairRead
{
    [JsonPropertyName("time_slot")] public required TimeSlot TimeSlot { get; init; }
    [JsonPropertyName("groups")] public required GroupBase[] Groups { get; init; }
    [JsonPropertyName("discipline")] public required DisciplineBase Discipline { get; init; }
    [JsonPropertyName("teachers")] public required TeacherBase[] Teachers { get; init; }
    [JsonPropertyName("rooms")] public required RoomBase[] Rooms { get; init; }
}