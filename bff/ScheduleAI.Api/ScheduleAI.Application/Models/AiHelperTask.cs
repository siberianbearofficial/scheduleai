using ScheduleAi.AiHelper;
using ScheduleAI.Core.Models;

namespace ScheduleAI.Application.Models;

internal class AiHelperTask : IAiHelperTask
{
    public AiHelperRequestContext? RequestContext { get; set; }

    public required Guid Id { get; init; }
    public required string Prompt { get; init; }
    public required AiHelperTaskStatus Status { get; set; }
    public AiHelperResponseModel? Response { get; set; }
    public IEnumerable<AiHelperToolCall> ToolCalls => RequestContext?.ToolCalls ?? [];
    public DateTime? StartedAt { get; set; }
    public DateTime? FinishedAt { get; set; }
}