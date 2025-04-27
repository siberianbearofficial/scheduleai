namespace ScheduleAI.Core.Models;

public class AiHelperResponseModel
{
    public required string Text { get; init; }
    public Pair[]? Pairs { get; init; }
}