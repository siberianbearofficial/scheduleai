using Newtonsoft.Json;

namespace AiHelper.Client.ToolParameters;

public class GetGroupScheduleParams
{
    [JsonProperty("group")] public required string GroupName { get; init; }
    [JsonProperty("date")] public required DateOnly Date { get; init; }
}