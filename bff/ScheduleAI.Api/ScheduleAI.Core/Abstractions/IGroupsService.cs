using ScheduleAI.Core.Models;

namespace ScheduleAI.Core.Abstractions;

public interface IGroupsService
{
    public Task<IEnumerable<Group>> GetGroupByIdAsync(Guid universityId, string teacherId);
    public Task<IEnumerable<Group>> GetGroupsAsync(Guid universityId);
    public Task<IEnumerable<Group>> GetGroupsAsync(Guid universityId, string abbr);
}