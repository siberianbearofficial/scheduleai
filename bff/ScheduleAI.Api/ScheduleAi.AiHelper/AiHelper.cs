using System.Text.Json;
using ScheduleAI.Core.Abstractions;
using ScheduleAI.Core.Models;

namespace ScheduleAi.AiHelper;

public class AiHelper : AiHelperClientBase<AiHelperRequestContext>
{
    private readonly IUniversityService _universityService;
    private readonly IScheduleService _scheduleService;
    private readonly IGroupsService _groupsService;
    private readonly ITeachersService _teachersService;

    public AiHelper(
        IUniversityService universityService,
        IScheduleService scheduleService,
        IGroupsService groupsService,
        ITeachersService teachersService
    ) : base(new Uri(Environment.GetEnvironmentVariable("AI_PROXY_URL") ??
                     throw new Exception("AI_PROXY_URL environment variable is not set")))
    {
        _universityService = universityService;
        _scheduleService = scheduleService;
        _teachersService = teachersService;
        _groupsService = groupsService;
    }

    protected override Task<IAiHelperClient.Pair[]> GetGroupSchedule(string? groupId,
        DateTime from,
        DateTime to, AiHelperRequestContext? context)
    {
        ArgumentNullException.ThrowIfNull(context);
        return ProcessFunction(
            context,
            nameof(GetGroupSchedule),
            () => _GetGroupSchedule(groupId, from, to, context),
            new
            {
                groupId,
                from,
                to,
            }
        );
    }

    private async Task<IAiHelperClient.Pair[]> _GetGroupSchedule(string? groupId,
        DateTime from,
        DateTime to, AiHelperRequestContext context)
    {
        return (await _scheduleService.GetGroupScheduleAsync(context.UniversityId,
                groupId ?? context.GroupId, from,
                to))
            .Select(PairToAiModel)
            .ToArray();
    }

    protected override Task<IAiHelperClient.Pair[]> GetMergedSchedule(string? groupId,
        string teacherId,
        DateTime from, DateTime to, AiHelperRequestContext? context)
    {
        ArgumentNullException.ThrowIfNull(context);
        return ProcessFunction(
            context,
            nameof(GetMergedSchedule),
            () => _GetMergedSchedule(groupId, teacherId, from, to, context),
            new
            {
                groupId,
                teacherId,
                from,
                to,
            }
        );
    }

    private async Task<IAiHelperClient.Pair[]> _GetMergedSchedule(string? groupId,
        string teacherId,
        DateTime from, DateTime to, AiHelperRequestContext context)
    {
        return (await _scheduleService.GetMergedScheduleAsync(context.UniversityId,
                groupId ?? context.GroupId,
                teacherId, from, to))
            .Select(PairToAiModel)
            .ToArray();
    }

    protected override Task<IAiHelperClient.Teacher[]> GetTeachersByGroup(string? groupId,
        AiHelperRequestContext? context)
    {
        ArgumentNullException.ThrowIfNull(context);
        return ProcessFunction(
            context,
            nameof(GetTeachersByGroup),
            () => _GetTeachersByGroup(groupId, context),
            new { groupId }
        );
    }

    private async Task<IAiHelperClient.Teacher[]> _GetTeachersByGroup(string? groupId,
        AiHelperRequestContext? context)
    {
        ArgumentNullException.ThrowIfNull(context);
        return (await _teachersService.GetTeachersByGroupAsync(context.UniversityId,
                groupId ?? context.GroupId))
            .Select(TeacherToAiModel)
            .ToArray();
    }

    protected override Task<IAiHelperClient.Teacher[]> GetTeachersByName(string substring,
        AiHelperRequestContext? context)
    {
        ArgumentNullException.ThrowIfNull(context);
        return ProcessFunction(
            context,
            nameof(GetTeachersByName),
            () => _GetTeachersByName(substring, context),
            new { substring }
        );
    }

    private async Task<IAiHelperClient.Teacher[]> _GetTeachersByName(string substring,
        AiHelperRequestContext? context)
    {
        ArgumentNullException.ThrowIfNull(context);
        return (await _teachersService.GetTeachersAsync(context.UniversityId, substring))
            .Select(TeacherToAiModel)
            .ToArray();
    }

    protected override Task<IAiHelperClient.Pair[]> GetTeacherSchedule(string teacherId,
        DateTime from,
        DateTime to, AiHelperRequestContext? context)
    {
        ArgumentNullException.ThrowIfNull(context);
        return ProcessFunction(
            context,
            nameof(GetTeacherSchedule),
            () => _GetTeacherSchedule(teacherId, from, to, context),
            new { teacherId, from, to }
        );
    }

    private async Task<IAiHelperClient.Pair[]> _GetTeacherSchedule(string teacherId,
        DateTime from,
        DateTime to, AiHelperRequestContext? context)
    {
        ArgumentNullException.ThrowIfNull(context);
        return (await _scheduleService.GetTeacherScheduleAsync(context.UniversityId, teacherId, from,
                to))
            .Select(PairToAiModel)
            .ToArray();
    }

    protected override Task<IAiHelperClient.Group[]> GetGroupsByName(string substring, AiHelperRequestContext? context)
    {
        ArgumentNullException.ThrowIfNull(context);
        return ProcessFunction(
            context,
            nameof(GetGroupsByName),
            () => _GetGroupsByName(substring, context),
            new { substring }
        );
    }

    private const int MaxGroupsCount = 10;

    private async Task<IAiHelperClient.Group[]> _GetGroupsByName(string substring, AiHelperRequestContext? context)
    {
        ArgumentNullException.ThrowIfNull(context);
        var result = (await _groupsService.GetGroupsAsync(context.UniversityId, substring))
            .Select(e => new IAiHelperClient.Group
            {
                Id = e.Id,
                Name = e.Name,
            })
            .ToArray();
        if (result.Length > MaxGroupsCount)
            throw new Exception("Too many groups found");
        return result;
    }

    protected override Task<string> GetGroupNameTemplate(AiHelperRequestContext? context)
    {
        return ProcessFunction(
            context,
            nameof(GetGroupNameTemplate),
            () => _GetGroupNameTemplate(context),
            new { }
        );
    }

    private async Task<string> _GetGroupNameTemplate(AiHelperRequestContext? context)
    {
        ArgumentNullException.ThrowIfNull(context);
        var university = _universityService.GetUniversity(context.UniversityId);
        return await university.GetGroupNameTemplate() ??
               throw new Exception("This university doesn't support group name template");
    }

    private static async Task<TResult> ProcessFunction<TResult>(AiHelperRequestContext? context, string name,
        Func<Task<TResult>> handler, object parameters)
    {
        ArgumentNullException.ThrowIfNull(context);
        try
        {
            var result = await handler();
            context.ToolCalls.Add(new AiHelperToolCall
            {
                ToolName = name,
                Parameter = JsonSerializer.Serialize(parameters),
                IsSuccess = true,
                Result = JsonSerializer.Serialize(result)
            });
            return result;
        }
        catch (Exception e)
        {
            context.ToolCalls.Add(new AiHelperToolCall
            {
                ToolName = name,
                Parameter = JsonSerializer.Serialize(parameters),
                IsSuccess = false,
                ErrorMessage = e.Message
            });
            throw;
        }
    }

    private static IAiHelperClient.Pair PairToAiModel(Pair pair)
    {
        return new IAiHelperClient.Pair
        {
            Discipline = pair.Discipline ?? "",
            ActType = pair.ActType,
            Teachers = pair.Teachers,
            GroupIds = pair.Groups,
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
            Groups = pair.GroupIds,
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