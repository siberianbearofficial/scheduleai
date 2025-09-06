namespace ScheduleAI.Core.Abstractions.Universities;

public interface IUniversity
{
    public string Name { get; }
    public string Id { get; }

    public Task<IEnumerable<IUniversityGroup>> GetGroups();

    public Task<IUniversityGroup> GetGroupById(string groupId,
        CancellationToken cancellationToken = default);

    public Task<IEnumerable<IUniversityGroup>> FindGroups(string groupName,
        CancellationToken cancellationToken = default);

    public Task<IEnumerable<IUniversityPair>> GetGroupSchedule(string groupId, DateTime startDate, DateTime endDate,
        CancellationToken cancellationToken = default);

    public Task<IUniversityTeacher> GetTeacherById(string teacherId,
        CancellationToken cancellationToken = default);

    public Task<IEnumerable<IUniversityTeacher>> FindTeachers(string fullName,
        CancellationToken cancellationToken = default);

    public Task<IEnumerable<IUniversityTeacher>> GetTeachers(CancellationToken cancellationToken = default);

    public Task<IEnumerable<IUniversityPair>> GetTeacherSchedule(string teacherId, DateTime startDate, DateTime endDate,
        CancellationToken cancellationToken = default);

    public Task<IEnumerable<IUniversityTeacher>> GetTeachersByGroup(string groupId,
        CancellationToken cancellationToken = default);

    public Task<string?> GetGroupNameTemplate() => Task.FromResult<string?>(null);

    public Task<TimeZoneInfo> GetTimeZone() => Task.FromResult<TimeZoneInfo>(TimeZoneInfo.Utc);
}