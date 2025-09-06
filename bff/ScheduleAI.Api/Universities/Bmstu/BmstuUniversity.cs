using BmstuSchedule.Client;
using ScheduleAI.Core.Abstractions.Universities;

namespace Bmstu;

public class BmstuUniversity : IUniversity
{
    private readonly BmstuScheduleClient _client = new(Environment.GetEnvironmentVariable("BMSTU_SCHEDULE_API_URL") ??
                                                       "https://bmstu-schedule-api.nachert.art/");

    public string Name => "МГТУ им. Баумана";
    public string Id => "Bmstu";

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

    public async Task<IEnumerable<IUniversityPair>> GetGroupSchedule(string groupId, DateTime startDate,
        DateTime endDate,
        CancellationToken cancellationToken = default)
    {
        return (await _client.GetGroupSchedule(Convert.ToInt32(groupId), startDate, endDate)).Data.Schedule
            .Select(BmstuPair.FromApiModel);
    }

    public async Task<IUniversityTeacher> GetTeacherById(string teacherId,
        CancellationToken cancellationToken = default)
    {
        return BmstuTeacher.FromModel((await _client.GetTeacherById(Convert.ToInt32(teacherId))).Data);
    }

    public async Task<IEnumerable<IUniversityTeacher>> FindTeachers(string fullName,
        CancellationToken cancellationToken = default)
    {
        return (await _client.GetTeachers(name: fullName)).Data.Items.Select(BmstuTeacher.FromModel);
    }

    public async Task<IEnumerable<IUniversityPair>> GetTeacherSchedule(string teacherId, DateTime startDate,
        DateTime endDate,
        CancellationToken cancellationToken = default)
    {
        return (await _client.GetTeacherSchedule(Convert.ToInt32(teacherId), startDate, endDate)).Data.Schedule
            .Select(BmstuPair.FromApiModel);
    }

    public async Task<IEnumerable<IUniversityTeacher>> GetTeachers(CancellationToken cancellationToken = default)
    {
        return (await _client.GetTeachers()).Data.Items.Select(BmstuTeacher.FromModel);
    }

    public Task<IEnumerable<IUniversityTeacher>> GetTeachersByGroup(string groupId,
        CancellationToken cancellationToken = default)
    {
        throw new NotSupportedException("Not implemented in API");
    }

    public Task<string?> GetGroupNameTemplate()
    {
        return Task.FromResult<string?>("""
                                        - Идентификатор факультета. 1 - 3 буквы
                                        - Индекс кафедры. 1 - 2 цифры
                                        - Дефис `-`
                                        - Номер семестра. 1 цифра
                                        - Индекс группы в потоке. 1 цифра.
                                        - Дополнительные обозначения:
                                            - `Б` - обучение по программе бакалавриата
                                            - `М` - магистратура
                                            - Отсутствие `Б` и `М` обозначает обучение по программе специалитета
                                            - `И` - группа для иностранных студентов
                                            - `В` - вечерняя группа
                                        """);
    }

    public Task<TimeZoneInfo> GetTimeZone()
    {
        return Task.FromResult(
            TimeZoneInfo.CreateCustomTimeZone("moscow", TimeSpan.FromHours(3), "МСК", "Московское время"));
    }
}