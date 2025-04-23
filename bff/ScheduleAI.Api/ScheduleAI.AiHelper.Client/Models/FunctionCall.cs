using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AiHelper.Client.Models;

public class FunctionCall
{
    [JsonPropertyName("name")]
    [JsonProperty("name")]
    public required string Name { get; init; }

    [JsonPropertyName("arguments")]
    [JsonProperty("arguments")]
    public required string Arguments { get; init; }
}