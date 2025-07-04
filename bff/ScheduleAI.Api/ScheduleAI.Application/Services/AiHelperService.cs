﻿using System.Text.Json;
using ScheduleAi.AiHelper;
using ScheduleAI.Core.Abstractions;
using ScheduleAI.Core.Models;

namespace ScheduleAI.Application.Services;

public class AiHelperService(
    IGroupsService groupsService,
    ITeachersService teachersService,
    IScheduleService scheduleService) : IAiHelperService
{
    private readonly Dictionary<Guid, TaskRecord> _tasks = [];

    private record TaskRecord(AiHelperTask AiHelperTask, Task AsyncTask, DateTime StartedAt);

    public Task<AiHelperTask> AskHelper(string prompt, string universityId, string groupId)
    {
        ClearOldTasks();
        var task = new AiHelperTask
        {
            Id = Guid.NewGuid(),
            Prompt = prompt,
            Status = AiHelperTaskStatus.NotStarted,
        };
        _tasks.Add(task.Id, new TaskRecord(task, StartTask(task, universityId, groupId), DateTime.UtcNow));
        return Task.FromResult(task);
    }

    private async Task StartTask(AiHelperTask task, string universityId, string groupId)
    {
        try
        {
            task.Status = AiHelperTaskStatus.InProgress;
            task.StartedAt = DateTime.UtcNow;

            var group = await groupsService.GetGroupByIdAsync(universityId, groupId);
            IAiHelperClient helperClient = new AiHelper(universityId, scheduleService, groupsService, teachersService);
            var resp = await helperClient.Request(new IAiHelperClient.AiHelperRequest
            {
                Text = task.Prompt,
                CurrentTime = DateTime.UtcNow,
                Group = group.Name,
            }, onToolCalled: args => task.ToolCalls.Add(new AiHelperToolCall
            {
                ToolName = args.ToolName,
                ToolDescription = args.ToolDescription,
                Parameter = args.Parameter,
                IsSuccess = args.IsSuccess,
                ErrorMessage = args.Exception?.Message,
                Result = JsonSerializer.Serialize(args.Result),
            }));
            if (resp == null)
                throw new Exception("LLM returned invalid response");
            task.Response = new AiHelperResponseModel
            {
                Text = resp.Text,
                Pairs = resp.Pairs?.Select(AiHelper.PairFromAiModel).ToArray()
            };
            task.Status = AiHelperTaskStatus.Completed;
            task.FinishedAt = DateTime.UtcNow;
        }
        catch (Exception e)
        {
            task.Status = AiHelperTaskStatus.Failed;
            task.FinishedAt = DateTime.UtcNow;
            Console.WriteLine(e);
        }
    }

    public Task<AiHelperTask> GetTask(Guid taskId)
    {
        return Task.FromResult(_tasks[taskId].AiHelperTask);
    }

    private static TimeSpan TaskLifeTime { get; } = TimeSpan.FromDays(1);

    private void ClearOldTasks()
    {
        foreach (var task in _tasks.Values.Where(t =>
                         DateTime.UtcNow - t.StartedAt > TaskLifeTime)
                     .ToArray())
        {
            _tasks.Remove(task.AiHelperTask.Id);
        }
    }
}