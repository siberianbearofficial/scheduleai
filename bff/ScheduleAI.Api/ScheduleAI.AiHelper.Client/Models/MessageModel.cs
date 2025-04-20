using Newtonsoft.Json;

namespace AiHelper.Client.Models;

public class MessageModel
{
    [JsonConverter(typeof(OpenAIRole))][JsonProperty("role")] public required OpenAIRole Role { get; init; }
    [JsonProperty("content")] public string? Content { get; init; }
}