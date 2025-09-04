using System.Text.Json;
using ScheduleAI.Core.Abstractions;
using ScheduleAI.Core.Models;

namespace ScheduleAi.AiHelper;

public class AiHelper : AiHelperClientBase<AiHelperRequestContext>
{
    private readonly IScheduleService _scheduleService;
    private readonly IGroupsService _groupsService;
    private readonly ITeachersService _teachersService;

    public AiHelper(
        IScheduleService scheduleService,
        IGroupsService groupsService,
        ITeachersService teachersService
    ) : base(new Uri(Environment.GetEnvironmentVariable("AI_PROXY_URL") ??
                     throw new Exception("AI_PROXY_URL environment variable is not set")))
    {
        _scheduleService = scheduleService;
        _teachersService = teachersService;
        _groupsService = groupsService;
    }

    protected override Task<IAiHelperClient.Pair[]> GetGroupSchedule(string? groupName,
        DateTime from,
        DateTime to, AiHelperRequestContext? context)
    {
        ArgumentNullException.ThrowIfNull(context);
        return ProcessFunction(
            context,
            nameof(GetGroupSchedule),
            () => _GetGroupSchedule(groupName, from, to, context),
            new
            {
                group = groupName,
                from,
                to,
            }
        );
    }

    private async Task<IAiHelperClient.Pair[]> _GetGroupSchedule(string? groupName,
        DateTime from,
        DateTime to, AiHelperRequestContext context)
    {
        var group = groupName == null
            ? null
            : (await _groupsService.GetGroupsAsync(context.UniversityId, groupName)).Single();
        return (await _scheduleService.GetGroupScheduleAsync(context.UniversityId,
                group?.Id ?? context.GroupId, from,
                to))
            .Select(PairToAiModel)
            .ToArray();
    }

    protected override Task<IAiHelperClient.Pair[]> GetMergedSchedule(string? groupName,
        string teacherId,
        DateTime from, DateTime to, AiHelperRequestContext? context)
    {
        ArgumentNullException.ThrowIfNull(context);
        return ProcessFunction(
            context,
            nameof(GetMergedSchedule),
            () => _GetMergedSchedule(groupName, teacherId, from, to, context),
            new
            {
                group = groupName,
                teacherId,
                from,
                to,
            }
        );
    }

    private async Task<IAiHelperClient.Pair[]> _GetMergedSchedule(string? groupName,
        string teacherId,
        DateTime from, DateTime to, AiHelperRequestContext context)
    {
        var group = groupName == null
            ? null
            : (await _groupsService.GetGroupsAsync(context.UniversityId, groupName)).Single();
        return (await _scheduleService.GetMergedScheduleAsync(context.UniversityId,
                group?.Id ?? context.GroupId,
                teacherId, from, to))
            .Select(PairToAiModel)
            .ToArray();
    }

    protected override Task<IAiHelperClient.Teacher[]> GetTeachersByGroup(string? groupName,
        AiHelperRequestContext? context)
    {
        ArgumentNullException.ThrowIfNull(context);
        return ProcessFunction(
            context,
            nameof(GetTeachersByGroup),
            () => _GetTeachersByGroup(groupName, context),
            new { group = groupName }
        );
    }

    private async Task<IAiHelperClient.Teacher[]> _GetTeachersByGroup(string? groupName,
        AiHelperRequestContext? context)
    {
        ArgumentNullException.ThrowIfNull(context);

        var group = groupName == null
            ? null
            : (await _groupsService.GetGroupsAsync(context.UniversityId, groupName)).Single();
        return (await _teachersService.GetTeachersByGroupAsync(context.UniversityId,
                group?.Id ?? context.GroupId))
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