using System.Text.Json.Serialization;

namespace BmstuSchedule.Client.Models;

public class TimeSlot
{
    [JsonPropertyName("start_time")] public required DateTime StartTime { get; init; }
    [JsonPropertyName("end_time")] public required DateTime EndTime { get; init; }
}