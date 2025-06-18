using ScheduleAI.Core.Abstractions;
using ScheduleAI.Core.Models;

namespace ScheduleAi.AiHelper;

public class AiHelper : AiHelperClientBase
{
    private readonly string _universityId;
    private readonly IScheduleService _scheduleService;
    private readonly ITeachersService _teachersService;

    public AiHelper(
        string universityId,
        IScheduleService scheduleService,
        ITeachersService teachersService
    ) : base(new Uri("https://simple-openai-proxy.nachert.art"))
    {
        _universityId = universityId;
        _scheduleService = scheduleService;
        _teachersService = teachersService;
    }

    protected override async Task<IAiHelperClient.Pair[]> GetGroupSchedule(string group, DateTime from, DateTime to)
    {
        return (await _scheduleService.GetGroupScheduleAsync(_universityId, group, from, to))
            .Select(PairToAiModel)
            .ToArray();
    }

    protected override async Task<IAiHelperClient.Pair[]> GetMergedSchedule(string group, string teacherId,
        DateTime from, DateTime to)
    {
        return (await _scheduleService.GetMergedScheduleAsync(_universityId, group, teacherId, from, to))
            .Select(PairToAiModel)
            .ToArray();
    }

    protected override async Task<IAiHelperClient.Teacher[]> GetTeachersByGroup(string group)
    {
        return (await _teachersService.GetTeachersByGroupAsync(_universityId, group))
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

    private static IAiHelperClient.Teacher TeacherToAiModel(Teacher teacher)
    {
        return new IAiHelperClient.Teacher
        {
            Id = teacher.Id,
            FullName = teacher.FullName,
        };
    }
}