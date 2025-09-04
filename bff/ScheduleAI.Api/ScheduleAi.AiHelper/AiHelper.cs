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

    protected override async Task<IAiHelperClient.Pair[]> GetGroupSchedule(string? groupName,
        DateTime from,
        DateTime to, AiHelperRequestContext? context)
    {
        ArgumentNullException.ThrowIfNull(context);
        var parameters = JsonSerializer.Serialize(new
        {
            group = groupName,
            from,
            to,
        });
        try
        {
            var group = groupName == null
                ? null
                : (await _groupsService.GetGroupsAsync(context.UniversityId, groupName)).Single();
            var result = (await _scheduleService.GetGroupScheduleAsync(context.UniversityId,
                    group?.Id ?? context.GroupId, from,
                    to))
                .Select(PairToAiModel)
                .ToArray();
            context.ToolCalls.Add(new AiHelperToolCall
            {
                ToolName = nameof(GetGroupSchedule),
                Parameter = parameters,
                IsSuccess = true,
                Result = JsonSerializer.Serialize(result)
            });
            return result;
        }
        catch (Exception e)
        {
            context.ToolCalls.Add(new AiHelperToolCall
            {
                ToolName = nameof(GetGroupSchedule),
                Parameter = parameters,
                IsSuccess = false,
                ErrorMessage = e.Message
            });
            throw;
        }
    }

    protected override async Task<IAiHelperClient.Pair[]> GetMergedSchedule(string? groupName,
        string teacherId,
        DateTime from, DateTime to, AiHelperRequestContext? context)
    {
        ArgumentNullException.ThrowIfNull(context);
        var parameters = JsonSerializer.Serialize(new
        {
            group = groupName,
            teacherId,
            from,
            to,
        });
        try
        {
            var group = groupName == null
                ? null
                : (await _groupsService.GetGroupsAsync(context.UniversityId, groupName)).Single();
            var result = (await _scheduleService.GetMergedScheduleAsync(context.UniversityId,
                    group?.Id ?? context.GroupId,
                    teacherId, from, to))
                .Select(PairToAiModel)
                .ToArray();
            context.ToolCalls.Add(new AiHelperToolCall
            {
                ToolName = nameof(GetMergedSchedule),
                Parameter = parameters,
                IsSuccess = true,
                Result = JsonSerializer.Serialize(result)
            });
            return result;
        }
        catch (Exception e)
        {
            context.ToolCalls.Add(new AiHelperToolCall
            {
                ToolName = nameof(GetMergedSchedule),
                Parameter = parameters,
                IsSuccess = false,
                ErrorMessage = e.Message
            });
            throw;
        }
    }

    protected override async Task<IAiHelperClient.Teacher[]> GetTeachersByGroup(string? groupName,
        AiHelperRequestContext? context)
    {
        ArgumentNullException.ThrowIfNull(context);
        var parameters = JsonSerializer.Serialize(new { group = groupName });
        try
        {
            var group = groupName == null
                ? null
                : (await _groupsService.GetGroupsAsync(context.UniversityId, groupName)).Single();
            var result = (await _teachersService.GetTeachersByGroupAsync(context.UniversityId,
                    group?.Id ?? context.GroupId))
                .Select(TeacherToAiModel)
                .ToArray();
            context.ToolCalls.Add(new AiHelperToolCall
            {
                ToolName = nameof(GetTeachersByGroup),
                Parameter = parameters,
                IsSuccess = true,
                Result = JsonSerializer.Serialize(result)
            });
            return result;
        }
        catch (Exception e)
        {
            context.ToolCalls.Add(new AiHelperToolCall
            {
                ToolName = nameof(GetTeachersByGroup),
                Parameter = parameters,
                IsSuccess = false,
                ErrorMessage = e.Message
            });
            throw;
        }
    }

    protected override async Task<IAiHelperClient.Teacher[]> GetTeachersByName(string substring,
        AiHelperRequestContext? context)
    {
        ArgumentNullException.ThrowIfNull(context);
        var parameters = JsonSerializer.Serialize(new { substring });
        try
        {
            var result = (await _teachersService.GetTeachersAsync(context.UniversityId, substring))
                .Select(TeacherToAiModel)
                .ToArray();
            context.ToolCalls.Add(new AiHelperToolCall
            {
                ToolName = nameof(GetTeachersByName),
                Parameter = parameters,
                IsSuccess = true,
                Result = JsonSerializer.Serialize(result)
            });
            return result;
        }
        catch (Exception e)
        {
            context.ToolCalls.Add(new AiHelperToolCall
            {
                ToolName = nameof(GetTeachersByName),
                Parameter = parameters,
                IsSuccess = false,
                ErrorMessage = e.Message
            });
            throw;
        }
    }

    protected override async Task<IAiHelperClient.Pair[]> GetTeacherSchedule(string teacherId,
        DateTime from,
        DateTime to, AiHelperRequestContext? context)
    {
        ArgumentNullException.ThrowIfNull(context);
        var parameters = JsonSerializer.Serialize(new { teacherId, from, to });
        try
        {
            var result = (await _scheduleService.GetTeacherScheduleAsync(context.UniversityId, teacherId, from,
                    to))
                .Select(PairToAiModel)
                .ToArray();
            context.ToolCalls.Add(new AiHelperToolCall
            {
                ToolName = nameof(GetTeacherSchedule),
                Parameter = parameters,
                IsSuccess = true,
                Result = JsonSerializer.Serialize(result)
            });
            return result;
        }
        catch (Exception e)
        {
            context.ToolCalls.Add(new AiHelperToolCall
            {
                ToolName = nameof(GetTeacherSchedule),
                Parameter = parameters,
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