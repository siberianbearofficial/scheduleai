using ScheduleAI.Core.Abstractions;
using ScheduleAI.Core.Abstractions.Universities;
using ScheduleAI.Core.Models;

namespace ScheduleAI.Application.Services;

public class GroupsService(IUniversityService universityService) : IGroupsService
{
    public async Task<IEnumerable<Group>> GetGroupsAsync(string universityId, string abbr)
    {
        var university = universityService.GetUniversity(universityId);
        return (await university.FindGroups(abbr)).Select(UniversityGroupToGroup);
    }

    public async Task<IEnumerable<Group>> GetGroupsAsync(string universityId)
    {
        var university = universityService.GetUniversity(universityId);
        return (await university.GetGroups()).Select(UniversityGroupToGroup);
    }

    public async Task<Group> GetGroupByIdAsync(string universityId, string groupId)
    {
        var university = universityService.GetUniversity(universityId);
        return UniversityGroupToGroup(await university.GetGroupById(groupId));
    }

    private static Group UniversityGroupToGroup(IUniversityGroup universityGroup)
    {
        return new Group
        {
            Id = universityGroup.Id,
            Name = universityGroup.Name,
        };
    }
}