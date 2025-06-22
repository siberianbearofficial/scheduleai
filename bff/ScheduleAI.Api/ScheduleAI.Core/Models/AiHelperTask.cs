namespace ScheduleAI.Core.Models;

public class AiHelperTask
{
    public required Guid Id { get; init; }
    public required string Prompt { get; init; }
    public required AiHelperTaskStatus Status { get; set; }
    public AiHelperResponseModel? Response { get; set; }
    public List<AiHelperToolCall> ToolCalls { get; } = [];
    public DateTime? StartedAt { get; set; }
    public DateTime? FinishedAt { get; set; }
}

public class AiHelperToolCall
{
    public required string ToolName { get; init; }
    public string? ToolDescription { get; init; }
    public required string Parameter { get; init; }
    public bool IsSuccess { get; init; }
    public string? Result { get; init; }
    public string? ErrorMessage { get; init; }
}

public enum AiHelperTaskStatus
{
    NotStarted,
    InProgress,
    Completed,
    Failed
}