using ScheduleAI.Core.Abstractions;
using ScheduleAI.Core.Models;

namespace ScheduleAi.AiHelper;

public class AiHelper : AiHelperClientBase
{
    private readonly string _universityId;
    private readonly IScheduleService _scheduleService;
    private readonly IGroupsService _groupsService;
    private readonly ITeachersService _teachersService;

    public AiHelper(
        string universityId,
        IScheduleService scheduleService,
        IGroupsService groupsService,
        ITeachersService teachersService
    ) : base(new Uri("https://simple-openai-proxy.nachert.art"))
    {
        _universityId = universityId;
        _scheduleService = scheduleService;
        _teachersService = teachersService;
        _groupsService = groupsService;
    }

    protected override async Task<IAiHelperClient.Pair[]> GetGroupSchedule(string groupName, DateTime from, DateTime to)
    {
        var group = (await _groupsService.GetGroupsAsync(_universityId, groupName)).Single();
        return (await _scheduleService.GetGroupScheduleAsync(_universityId, group.Id, from, to))
            .Select(PairToAiModel)
            .ToArray();
    }

    protected override async Task<IAiHelperClient.Pair[]> GetMergedSchedule(string groupName, string teacherId,
        DateTime from, DateTime to)
    {
        var group = (await _groupsService.GetGroupsAsync(_universityId, groupName)).Single();
        return (await _scheduleService.GetMergedScheduleAsync(_universityId, group.Id, teacherId, from, to))
            .Select(PairToAiModel)
            .ToArray();
    }

    protected override async Task<IAiHelperClient.Teacher[]> GetTeachersByGroup(string groupName)
    {
        var group = (await _groupsService.GetGroupsAsync(_universityId, groupName)).Single();
        return (await _teachersService.GetTeachersByGroupAsync(_universityId, group.Id))
            .Select(TeacherToAiModel)
            .ToArray();
    }

    protected override async Task<IAiHelperClient.Teacher[]> GetTeachersByName(string substring)
    {
        return (await _teachersService.GetTeachersAsync(_universityId, substring))
            .Select(TeacherToAiModel)
            .ToArray();
    }

    protected override async Task<IAiHelperClient.Pair[]> GetTeacherSchedule(string teacherId, DateTime from,
        DateTime to)
    {
        return (await _scheduleService.GetTeacherScheduleAsync(_universityId, teacherId, from, to))
            .Select(PairToAiModel)
            .ToArray();
    }

    private static IAiHelperClient.Pair PairToAiModel(Pair pair)
    {
        return new IAiHelperClient.Pair
        {
            Discipline = pair.Discipline ?? "",
            ActType = pair.ActType,
            Teachers = pair.Teachers,
            Groups = pair.Groups,
            Rooms = pair.Rooms,
            StartTime = pair.StartTime,
            EndTime = pair.EndTime,
            Convenience = pair.Convenience == null
                ? null
                : new IAiHelperClient.PairConvenience
                {
                    Coefficient = pair.Convenience.Coefficient,
                    Status = (int)pair.Convenience.Status,
                    WaitTime = pair.Convenience.WaitTime,
                    Collisions = pair.Convenience.Collisions.Select(PairToAiModel).ToArray()
                },
        };
    }

    public static Pair PairFromAiModel(IAiHelperClient.Pair pair)
    {
        return new Pair
        {
            Discipline = pair.Discipline,
            ActType = pair.ActType,
            Teachers = pair.Teachers,
            Groups = pair.Groups,
            Rooms = pair.Rooms,
            StartTime = pair.StartTime,
            EndTime = pair.EndTime,
            Convenience = pair.Convenience == null
                ? null
                : new PairConvenience
                {
                    Coefficient = pair.Convenience.Coefficient,
                    Status = (MergedPairStatus)pair.Convenience.Status,
                    WaitTime = pair.Convenience.WaitTime,
                    Collisions = pair.Convenience.Collisions.Select(PairFromAiModel).ToArray()
                },
        };
    }

    private static IAiHelperClient.Teacher TeacherToAiModel(Teacher teacher)
    {
        return new IAiHelperClient.Teacher
        {
            Id = teacher.Id,
            FullName = teacher.FullName,
        };
    }
}