using ScheduleAI.Core.Abstractions.Universities;

namespace Bmstu.Mock;

public class BmstuUniversity : IUniversity
{
    public string Name => "МГТУ им. Баумана";
    public string Id => "Bmstu";

    private readonly Parser _parser = new();

    public BmstuUniversity()
    {
        StartParsing();
    }

    private async void StartParsing()
    {
        try
        {
            await _parser.ParseAsync();
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
        }
    }

    public Task<IEnumerable<IUniversityGroup>> GetGroups()
    {
        return Task.FromResult<IEnumerable<IUniversityGroup>>(_parser.Groups);
    }

    public Task<IUniversityGroup> GetGroupById(string groupId, CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IUniversityGroup>(_parser.Groups.Single(g => g.Id == groupId));
    }

    public Task<IEnumerable<IUniversityGroup>> FindGroups(string groupName,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IEnumerable<IUniversityGroup>>(_parser.Groups.Where(g => g.Name.Contains(groupName)));
    }

    public Task<IEnumerable<IUniversityPair>> GetGroupSchedule(string groupId, DateTime startDate,
        DateTime endDate,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IEnumerable<IUniversityPair>>(_parser.Groups.Single(e => e.Id == groupId).Pairs);
    }

    public Task<IUniversityTeacher> GetTeacherById(string teacherId,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IUniversityTeacher>(_parser.Teachers.Single(t => t.Id == teacherId));
    }

    public Task<IEnumerable<IUniversityTeacher>> FindTeachers(string fullName,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IEnumerable<IUniversityTeacher>>(_parser.Teachers.Where(t => t.FullName.Contains(fullName)));
    }

    public Task<IEnumerable<IUniversityPair>> GetTeacherSchedule(string teacherId, DateTime startDate,
        DateTime endDate,
        CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IEnumerable<IUniversityPair>>(_parser.Teachers.Single(e => e.Id == teacherId).Pairs);
    }

    public Task<IEnumerable<IUniversityTeacher>> GetTeachers(CancellationToken cancellationToken = default)
    {
        return Task.FromResult<IEnumerable<IUniversityTeacher>>(_parser.Teachers);
    }

    public Task<IEnumerable<IUniversityTeacher>> GetTeachersByGroup(string groupId,
        CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException("Not implemented in API");
    }
}