using ScheduleAI.Core.Models;

namespace ScheduleAI.Core.Abstractions;

public interface IGroupsService
{
    public Task<Group> GetGroupByIdAsync(Guid universityId, string groupId);
    public Task<IEnumerable<Group>> GetGroupsAsync(Guid universityId);
    public Task<IEnumerable<Group>> GetGroupsAsync(Guid universityId, string abbr);
}