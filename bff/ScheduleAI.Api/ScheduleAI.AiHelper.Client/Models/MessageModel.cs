using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AiHelper.Client.Models;

public class MessageModel
{
    [JsonPropertyName("role")]
    [JsonProperty("role")]
    public string? Role { get; init; }

    [JsonProperty("content")]
    [JsonPropertyName("content")]
    public string? Content { get; init; }

    [JsonProperty("tool_call_id")]
    [JsonPropertyName("tool_call_id")]
    public string? ToolCallId { get; init; }

    [JsonProperty("tool_calls")]
    [JsonPropertyName("tool_calls")]
    public ToolCall[]? ToolCalls { get; init; }
}