using System.Text.Json.Serialization;

namespace ScheduleAI.Core.Models;

public interface IAiHelperTask
{
    public Guid Id { get; }
    public string Prompt { get; }
    public AiHelperTaskStatus Status { get; }
    public AiHelperResponseModel? Response { get; }
    public IEnumerable<AiHelperToolCall> ToolCalls { get; }
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