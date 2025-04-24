namespace ScheduleAI.Api.Schemas;

public class AiHelperRequestModel
{
    public required string UniversityId { get; init; }
    public required string GroupId { get; init; }
    public required string Prompt { get; init; }
}