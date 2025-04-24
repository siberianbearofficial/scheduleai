using ScheduleAI.Core.Models;

namespace ScheduleAI.Core.Abstractions;

public interface IGroupsService
{
    public Task<Group> GetGroupByIdAsync(string universityId, string groupId);
    public Task<IEnumerable<Group>> GetGroupsAsync(string universityId);
    public Task<IEnumerable<Group>> GetGroupsAsync(string universityId, string abbr);
}