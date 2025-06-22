using System.Text.Json;
using ScheduleAi.AiHelper;
using ScheduleAI.Core.Abstractions;
using ScheduleAI.Core.Models;

namespace ScheduleAI.Application.Services;

public class AiHelperService(
    IGroupsService groupsService,
    ITeachersService teachersService,
    IScheduleService scheduleService) : IAiHelperService
{
    private readonly Dictionary<Guid, AiHelperTask> _tasks = [];

    public Task<AiHelperTask> AskHelper(string prompt, string universityId, string groupId)
    {
        ClearOldTasks();
        var task = new AiHelperTask
        {
            Id = Guid.NewGuid(),
            Prompt = prompt,
            Status = AiHelperTaskStatus.NotStarted,
        };
        _tasks.Add(task.Id, task);
        StartTask(task, universityId, groupId);
        return Task.FromResult(task);
    }

    private async void StartTask(AiHelperTask task, string universityId, string groupId)
    {
        try
        {
            task.Status = AiHelperTaskStatus.InProgress;
            task.StartedAt = DateTime.Now;

            var group = await groupsService.GetGroupByIdAsync(universityId, groupId);
            IAiHelperClient helperClient = new AiHelper(universityId, scheduleService, groupsService, teachersService);
            var resp = await helperClient.Request(new IAiHelperClient.AiHelperRequest
            {
                Text = task.Prompt,
                CurrentTime = DateTime.Now,
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
            task.FinishedAt = DateTime.Now;
        }
        catch (Exception e)
        {
            task.Status = AiHelperTaskStatus.Failed;
            task.FinishedAt = DateTime.Now;
            Console.WriteLine(e);
        }
    }

    public Task<AiHelperTask> GetTask(Guid taskId)
    {
        return Task.FromResult(_tasks[taskId]);
    }

    private void ClearOldTasks()
    {
        foreach (var task in _tasks.Values.Where(t =>
                         t.FinishedAt != null && DateTime.Now - t.FinishedAt > TimeSpan.FromDays(1))
                     .ToArray())
        {
            _tasks.Remove(task.Id);
        }
    }
}