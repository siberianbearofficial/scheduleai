using ScheduleAi.AiHelper;
using ScheduleAI.Application.Models;
using ScheduleAI.Core.Abstractions;
using ScheduleAI.Core.Models;

namespace ScheduleAI.Application.Services;

public class AiHelperService(IGroupsService groupsService, IAiHelperClient helperClient) : IAiHelperService
{
    private readonly Dictionary<Guid, TaskRecord> _tasks = [];

    private record TaskRecord(IAiHelperTask AiHelperTask, Task AsyncTask, DateTime StartedAt);

    public Task<IAiHelperTask> AskHelper(string prompt, string universityId, string groupId)
    {
        ClearOldTasks();
        var task = new AiHelperTask
        {
            Id = Guid.NewGuid(),
            Prompt = prompt,
            Status = AiHelperTaskStatus.NotStarted,
        };
        _tasks.Add(task.Id, new TaskRecord(task, StartTask(task, universityId, groupId), DateTime.UtcNow));
        return Task.FromResult<IAiHelperTask>(task);
    }

    private async Task StartTask(AiHelperTask task, string universityId, string groupId)
    {
        try
        {
            task.Status = AiHelperTaskStatus.InProgress;
            task.StartedAt = DateTime.UtcNow;

            var group = await groupsService.GetGroupByIdAsync(universityId, groupId);

            var context = new AiHelperRequestContext
            {
                UniversityId = universityId,
                GroupId = groupId,
            };
            task.RequestContext = context;
            var resp = await helperClient.Request(new IAiHelperClient.AiHelperRequest
            {
                Text = task.Prompt,
                CurrentTime = DateTime.UtcNow,
                GroupId = groupId,
                GroupName = group.Name,
            }, context);
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

    public Task<IAiHelperTask> GetTask(Guid taskId)
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