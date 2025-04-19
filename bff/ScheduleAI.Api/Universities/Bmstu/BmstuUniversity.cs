using BmstuSchedule.Client;
using ScheduleAI.Core.Abstractions.Universities;

namespace Bmstu;

public class BmstuUniversity : IUniversity
{
    private readonly BmstuScheduleClient _client = new("https://bmstu-schedule-api.nachert.art/");

    public async Task<IEnumerable<IUniversityGroup>> GetGroups()
    {
        return (await _client.GetGroups()).Data.Items
            .Select(BmstuGroup.FromModel);
    }

    public async Task<IUniversityGroup> GetGroupById(string groupId, CancellationToken cancellationToken = default)
    {
        return BmstuGroup.FromModel((await _client.GetGroupById(Convert.ToInt32(groupId))).Data);
    }

    public async Task<IEnumerable<IUniversityGroup>> FindGroups(string groupName,
        CancellationToken cancellationToken = default)
    {
        return (await _client.GetGroups(abbr: groupName)).Data.Items
            .Select(BmstuGroup.FromModel);
    }

    public async Task<IEnumerable<IUniversityPair>> GetGroupSchedule(string groupId, DateTime startDate, DateTime endDate,
        CancellationToken cancellationToken = default)
    {
        return (await _client.GetSchedule(Convert.ToInt32(groupId), startDate, endDate)).Data.Schedule
            .Select(BmstuPair.FromApiModel);
    }

    public async Task<IEnumerable<IUniversityPair>> GetTeacherById(string teacherId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<IUniversityTeacher>> FindTeachers(string fullName,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<IUniversityPair>> GetTeacherSchedule(string teacherId, DateTime startDate, DateTime endDate,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<IUniversityTeacher>> GetTeachers(CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<IUniversityTeacher>> GetTeachersByGroup(string groupId,
        CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}