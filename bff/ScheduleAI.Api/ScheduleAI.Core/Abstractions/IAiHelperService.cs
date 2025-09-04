using ScheduleAI.Core.Models;

namespace ScheduleAI.Core.Abstractions;

public interface IAiHelperService
{
    public Task<IAiHelperTask> AskHelper(string prompt, string universityId, string groupId);

    public Task<IAiHelperTask> GetTask(Guid taskId);
}