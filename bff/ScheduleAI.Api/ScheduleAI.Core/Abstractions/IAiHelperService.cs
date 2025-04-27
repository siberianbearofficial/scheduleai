using ScheduleAI.Core.Models;

namespace ScheduleAI.Core.Abstractions;

public interface IAiHelperService
{
    public Task<AiHelperResponseModel> AskHelper(string prompt, string universityId, string groupId);
}