namespace ScheduleAI.Core.Abstractions;

public interface IAiHelperService
{
    public Task<string> AskHelper(string prompt, Guid universityId, string groupId);
}