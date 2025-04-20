using Newtonsoft.Json;

namespace AiHelper.Client.Models;

public class AgentRequestModel
{
    [JsonProperty("messages")] public required MessageModel[] Messages { get; init; }
}