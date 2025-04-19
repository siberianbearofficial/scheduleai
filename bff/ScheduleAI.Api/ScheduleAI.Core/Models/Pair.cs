namespace ScheduleAI.Core.Models;

public class Pair
{
    public string[] Teachers { get; init; } = [];
    public required string[] Groups { get; init; }
    public required DateTime StartTime { get; init; }
    public required DateTime EndTime { get; init; }
    public string[] Rooms { get; init; } = [];
    public string? Discipline { get; init; }
    public string? ActType { get; init; }
}