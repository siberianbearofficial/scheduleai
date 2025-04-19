namespace ScheduleAI.Api.Schemas;

public class AiHelperRequestModel
{
    public required Guid UniversityId { get; init; }
    public required string GroupId { get; init; }
    public required string Prompt { get; init; }
}