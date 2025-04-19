namespace ScheduleAI.Core.Abstractions.Universities;

public interface IUniversityPair
{
    public string[] Teachers { get; }
    public string[] Groups { get; }
    public DateTime StartTime { get; }
    public DateTime EndTime { get; }
    public string[] Rooms { get; }
    public string? Discipline { get; }
    public string? ActType { get; }
}