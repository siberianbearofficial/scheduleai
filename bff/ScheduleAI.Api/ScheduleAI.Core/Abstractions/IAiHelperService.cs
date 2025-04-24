namespace ScheduleAI.Core.Abstractions;

public interface IAiHelperService
{
    public Task<string> AskHelper(string prompt, string universityId, string groupId);
}