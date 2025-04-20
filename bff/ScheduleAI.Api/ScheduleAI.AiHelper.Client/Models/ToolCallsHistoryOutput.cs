using Newtonsoft.Json;

namespace AiHelper.Client.Models;

public class ToolCallsHistoryOutput
{
    [JsonProperty("tool_calls")] public ToolCall[]? ToolCalls { get; init; }
    [JsonProperty("role")] public string? Role { get; init; }
    [JsonProperty("content")] public string? Content { get; init; }
}