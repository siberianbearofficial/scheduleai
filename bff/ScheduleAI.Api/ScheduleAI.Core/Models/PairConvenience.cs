namespace ScheduleAI.Core.Models;

public class PairConvenience
{
    public required double Coefficient { get; init; }
    public Pair[] Collisions { get; init; } = [];
    public TimeSpan? WaitTime { get; init; }
    public MergedPairStatus Status { get; init; }
}