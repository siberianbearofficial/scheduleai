using ScheduleAI.Core.Abstractions.Universities;

namespace Bmstu.Mock;

public class BmstuPair : IUniversityPair
{
    public required string[] Teachers { get; init; }
    public required string[] Groups { get; init; }
    public DateTime StartTime { get; init; }
    public DateTime EndTime { get; init; }
    public string[] Rooms { get; init; } = [];

    public string? ActType { get; init; }
    public string? Discipline { get; init; }
}