using ScheduleAi.AiHelper;
using ScheduleAI.Core.Abstractions;
using ScheduleAI.Core.Models;

namespace ScheduleAI.Application.Services;

public class AiHelperService(
    IGroupsService groupsService,
    ITeachersService teachersService,
    IScheduleService scheduleService) : IAiHelperService
{
    public async Task<AiHelperResponseModel> AskHelper(string prompt, string universityId, string groupId)
    {
        var group = await groupsService.GetGroupByIdAsync(universityId, groupId);
        IAiHelperClient helperClient = new AiHelper(universityId, scheduleService, groupsService, teachersService);
        var resp = await helperClient.Request(new IAiHelperClient.AiHelperRequest
        {
            Text = prompt,
            CurrentTime = DateTime.Now,
            Group = group.Name,
        });
        if (resp == null)
            throw new Exception("LLM returned invalid response");
        return new AiHelperResponseModel
        {
            Text = resp.Text,
            Pairs = resp.Pairs?.Select(AiHelper.PairFromAiModel).ToArray()
        };
    }
}