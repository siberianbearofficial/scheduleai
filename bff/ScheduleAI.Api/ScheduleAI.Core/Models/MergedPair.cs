namespace ScheduleAI.Core.Models;

public class MergedPair
{
    public required DateTime StartTime { get; init; }
    public required DateTime EndTime { get; init; }
    public string? ActType { get; init; }
    public string? Discipline { get; init; }
    public string[] Rooms { get; init; } = [];

    public required double Convenience { get; init; }
    public Pair[] Collisions { get; init; } = [];
    public TimeSpan? WaitTime { get; init; }
    public MergedPairStatus Status { get; init; }
}