using Newtonsoft.Json;

namespace BmstuSchedule.Client.Models;

public class TimeSlot
{
    [JsonProperty("start_time")] public required DateTime StartTime { get; init; }
    [JsonProperty("end_time")] public required DateTime EndTime { get; init; }
}