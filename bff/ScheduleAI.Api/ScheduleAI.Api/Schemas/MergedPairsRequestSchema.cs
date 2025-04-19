namespace ScheduleAI.Api.Schemas;

public class MergedPairsRequestSchema
{
    public required Guid UniversityId { get; init; }
    public required string GroupId { get; init; }
    public required string TeacherId { get; init; }
    public required DateTime StartTime { get; init; }
    public required DateTime EndTime { get; init; }
}