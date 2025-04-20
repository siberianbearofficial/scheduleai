using Newtonsoft.Json;

namespace AiHelper.Client.Models;

public class AgentResponseModel
{
    [JsonProperty("messages")] public required MessageModel[] Messages { get; init; }
}