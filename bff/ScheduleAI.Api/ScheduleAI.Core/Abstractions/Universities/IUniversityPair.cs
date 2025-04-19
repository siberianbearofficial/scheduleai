namespace ScheduleAI.Core.Abstractions.Universities;

public interface IUniversityPair
{
    public string TeacherId { get; }
    public string GroupId { get; }
    public DateTime StartTime { get; }
    public DateTime EndTime { get; }
    public string? Room { get; }
    public string? Discipline { get; }
    public string? ActType { get; }
}