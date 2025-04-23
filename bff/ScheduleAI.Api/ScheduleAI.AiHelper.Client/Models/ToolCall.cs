using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AiHelper.Client.Models;

public class ToolCall
{
    [JsonPropertyName("id")]
    [JsonProperty("id")]
    public required string Id { get; init; }

    [JsonPropertyName("type")]
    [JsonProperty("type")]
    public string? Type { get; init; }

    [JsonPropertyName("function")]
    [JsonProperty("function")]
    public required FunctionCall Function { get; init; }
}