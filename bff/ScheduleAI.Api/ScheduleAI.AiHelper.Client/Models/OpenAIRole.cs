using Newtonsoft.Json;

namespace AiHelper.Client.Models;

public enum OpenAIRole
{
    [JsonProperty("system")] System,
    [JsonProperty("user")] User,
    [JsonProperty("assistant")] Assistant,
    [JsonProperty("tool")] Tool
}