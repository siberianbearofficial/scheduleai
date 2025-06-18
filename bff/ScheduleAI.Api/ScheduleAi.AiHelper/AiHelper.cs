using ScheduleAI.Core.Abstractions;

namespace ScheduleAi.AiHelper;

public class AiHelper : AiHelperClientBase
{
    private readonly IScheduleService _scheduleService;
    
    public AiHelper(IScheduleService scheduleService) : base(new Uri("https://simple-openai-proxy.nachert.art"))
    {
        _scheduleService = scheduleService;
    }

    protected override async Task<IAiHelperClient.Pair[]> GetGroupSchedule(string group, DateTime from, DateTime to)
    {
        throw new NotImplementedException();
    }

    protected override async Task<IAiHelperClient.Pair[]> GetMergedSchedule(string group, string teacherId, DateTime from, DateTime to)
    {
        throw new NotImplementedException();
    }

    protected override async Task<IAiHelperClient.Teacher[]> GetTeachersByGroup(string group)
    {
        throw new NotImplementedException();
    }

    protected override async Task<IAiHelperClient.Teacher[]> GetTeachersByName(string substring)
    {
        throw new NotImplementedException();
    }

    protected override async Task<IAiHelperClient.Pair[]> GetTeacherSchedule(string teacher_id, DateTime from, DateTime to)
    {
        throw new NotImplementedException();
    }
}