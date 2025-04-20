using Newtonsoft.Json;

namespace AiHelper.Client.Models;

public class ToolCall
{
    [JsonProperty("id")] public required string Id { get; init; }
    [JsonProperty("type")] public string? Type { get; init; }
    [JsonProperty("function")] public required FunctionCall Function { get; init; }
}