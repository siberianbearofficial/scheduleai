using Newtonsoft.Json;

namespace AiHelper.Client.Models;

public class FunctionCall
{
    [JsonProperty("name")] public required string Name { get; init; }
    [JsonProperty("arguments")] public required string Arguments { get; init; }
}