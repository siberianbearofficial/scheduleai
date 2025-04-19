namespace ScheduleAI.Core.Models;

public class MergedPair
{
    public required DateTime StartTime { get; init; }
    public required DateTime EndTime { get; init; }
    public string? ActType { get; init; }
    public string? Discipline { get; init; }
    public required int Convenience { get; init; }
    public string[] Rooms { get; init; } = [];
}