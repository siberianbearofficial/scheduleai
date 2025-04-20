using Newtonsoft.Json;

namespace AiHelper.Client.Models;

public class ValidationError
{
    [JsonProperty("loc")] public required string?[] Loc { get; init; }
    [JsonProperty("msg")] public required string Msg { get; init; }
    [JsonProperty("type")] public required string Type { get; init; }
}