using Newtonsoft.Json;

namespace AiHelper.Client.ToolParameters;

public class GetTeachersByNameParams
{
    [JsonProperty("substring")] public required string Substring { get; init; }
}