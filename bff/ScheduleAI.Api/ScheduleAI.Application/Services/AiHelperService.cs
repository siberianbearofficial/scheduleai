using ScheduleAI.Core.Abstractions;

namespace ScheduleAI.Application.Services;

public class AiHelperService : IAiHelperService
{
    public Task<string> AskHelper(string prompt, Guid universityId, string groupId)
    {
        return Task.FromResult("Данная функция пока недоступна");
    }
}