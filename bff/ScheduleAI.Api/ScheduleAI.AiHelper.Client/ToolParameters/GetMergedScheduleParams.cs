using Newtonsoft.Json;

namespace AiHelper.Client.ToolParameters;

public class GetMergedScheduleParams
{
    [JsonProperty("group")] public required string GroupName { get; init; }
    [JsonProperty("teacher_id")] public required string TeacherId { get; init; }
    [JsonProperty("date")] public required DateOnly Date { get; init; }
}