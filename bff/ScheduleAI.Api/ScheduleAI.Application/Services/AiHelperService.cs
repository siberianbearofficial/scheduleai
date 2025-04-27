using AiHelper.Client;
using AiHelper.Client.Models;
using AiHelper.Client.ToolParameters;
using Newtonsoft.Json;
using ScheduleAI.Core.Abstractions;
using ScheduleAI.Core.Models;

namespace ScheduleAI.Application.Services;

public class AiHelperService(
    IGroupsService groupsService,
    ITeachersService teachersService,
    IScheduleService scheduleService) : IAiHelperService
{
    private readonly AiHelperClient _client = new(Environment.GetEnvironmentVariable("AI_HELPER_URL") ??
                                                  throw new Exception("AI_HELPER_URL environment variable is not set"));

    public async Task<AiHelperResponseModel> AskHelper(string prompt, string universityId, string groupId)
    {
        var group = await groupsService.GetGroupByIdAsync(universityId, groupId);
        var resp = await _client.PostApiAgentRequest(null, universityId, group.Name, prompt);
        while (true)
        {
            var lastMessage = resp.Messages.Last();
            if (lastMessage.ToolCalls == null || lastMessage.ToolCalls.Length == 0)
            {
                return ConvertResponse(lastMessage);
            }

            var request = resp.Messages.ToList();
            foreach (var toolCall in lastMessage.ToolCalls)
            {
                request.Add(new MessageModel()
                {
                    Role = "tool",
                    Content = await CallFunc(toolCall, universityId, groupId),
                    ToolCallId = toolCall.Id,
                });
            }

            resp = await _client.PostApiAgentRequest(new AgentRequestModel
            {
                Messages = request.ToArray(),
            }, universityId, groupId);
        }
    }

    private async Task<string> CallFunc(ToolCall toolCall, string universityId, string groupId)
    {
        object? res;
        // return "None";
        switch (toolCall.Function.Name)
        {
            case "get_group_schedule":
                var getGroupScheduleParams =
                    JsonConvert.DeserializeObject<GetGroupScheduleParams>(toolCall.Function.Arguments);
                if (getGroupScheduleParams == null)
                    res = null;
                else
                    res = await scheduleService.GetGroupScheduleAsync(universityId, groupId,
                        getGroupScheduleParams.Date.ToDateTime(new TimeOnly()),
                        getGroupScheduleParams.Date.ToDateTime(new TimeOnly(23, 59, 59)));
                break;
            case "get_teacher_schedule":
                var getTeacherScheduleParams =
                    JsonConvert.DeserializeObject<GetTeacherScheduleParams>(toolCall.Function.Arguments);
                if (getTeacherScheduleParams == null)
                    res = null;
                else
                    res = await scheduleService.GetTeacherScheduleAsync(universityId,
                        getTeacherScheduleParams.TeacherId,
                        getTeacherScheduleParams.Date.ToDateTime(new TimeOnly()),
                        getTeacherScheduleParams.Date.ToDateTime(new TimeOnly(23, 59, 59)));
                break;
            case "get_teachers_by_name":
                var getTeachersByNameParams =
                    JsonConvert.DeserializeObject<GetTeachersByNameParams>(toolCall.Function.Arguments);
                if (getTeachersByNameParams == null)
                    res = null;
                else
                    res = await teachersService.GetTeachersAsync(universityId, getTeachersByNameParams.Substring);
                break;
            case "get_teachers_by_group":
                var getTeachersByGroupParams =
                    JsonConvert.DeserializeObject<GetTeachersByGroupParams>(toolCall.Function.Arguments);
                if (getTeachersByGroupParams == null)
                    res = null;
                else
                    res = await teachersService.GetTeachersByGroupAsync(universityId, groupId);
                break;
            case "get_merged_schedule":
                var getMergedScheduleParams =
                    JsonConvert.DeserializeObject<GetMergedScheduleParams>(toolCall.Function.Arguments);
                if (getMergedScheduleParams == null)
                    res = null;
                else
                    res = await scheduleService.GetMergedScheduleAsync(universityId, groupId,
                        getMergedScheduleParams.TeacherId, getMergedScheduleParams.Date.ToDateTime(new TimeOnly()),
                        getMergedScheduleParams.Date.ToDateTime(new TimeOnly(23, 59, 59)).Date);
                break;
            default:
                res = "None";
                break;
        }

        return JsonConvert.SerializeObject(res);
    }

    private static AiHelperResponseModel ConvertResponse(MessageModel lastMessage)
    {
        var gptResponseSource = lastMessage.Content ?? "{}";
        if (gptResponseSource.StartsWith("```json"))
            gptResponseSource = gptResponseSource.Remove(0, "```json".Length).Trim('`');
        var gptResponse = JsonConvert.DeserializeObject<GptResponseContent>(gptResponseSource);
        return new AiHelperResponseModel
        {
            Text = gptResponse?.Message ?? throw new Exception("Empty response"),
            Pairs = gptResponse.Pairs,
        };
    }

    private class GptResponseContent
    {
        [JsonProperty("message")] public string? Message { get; init; }

        [JsonProperty("schedule")] public Pair[]? Pairs { get; init; }

        [JsonProperty("function_calling_needed")]
        public bool? FunctionCallingNeeded { get; init; }

        [JsonProperty("clarification_needed")] public bool? CalrificationNeeded { get; init; }
    }
}