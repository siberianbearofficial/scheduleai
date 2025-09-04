using ScheduleAI.Core.Models;

namespace ScheduleAi.AiHelper;

public class AiHelperRequestContext
{
    public required string UniversityId { get; set; }
    public required string GroupId { get; set; }
    public List<AiHelperToolCall> ToolCalls { get; } = [];
}