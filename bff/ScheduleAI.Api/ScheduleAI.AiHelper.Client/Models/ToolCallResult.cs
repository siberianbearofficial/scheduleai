using Newtonsoft.Json;

namespace AiHelper.Client.Models;

public class ToolCallResult
{
    [JsonProperty("role")] public string? Role { get; init; }
    [JsonProperty("tool_call_id")] public required string ToolCallId { get; init; }
    [JsonProperty("content")] public required string Content { get; init; }
}