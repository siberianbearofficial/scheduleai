using Newtonsoft.Json;

namespace AiHelper.Client.ToolParameters;

public class GetTeachersByGroupParams
{
    [JsonProperty("group")] public required string Group { get; init; }
}