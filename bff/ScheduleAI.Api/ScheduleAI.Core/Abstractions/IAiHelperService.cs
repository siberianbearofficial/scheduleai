using ScheduleAI.Core.Models;

namespace ScheduleAI.Core.Abstractions;

public interface IAiHelperService
{
    public Task<AiHelperTask> AskHelper(string prompt, string universityId, string groupId);

    public Task<AiHelperTask> GetTask(Guid taskId);
}