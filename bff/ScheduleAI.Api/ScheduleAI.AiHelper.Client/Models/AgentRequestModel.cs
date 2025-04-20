using Newtonsoft.Json;

namespace AiHelper.Client.Models;

public class AgentRequestModel
{
    [JsonProperty("messages")] public required IInputMessage[] Messages { get; init; }
}