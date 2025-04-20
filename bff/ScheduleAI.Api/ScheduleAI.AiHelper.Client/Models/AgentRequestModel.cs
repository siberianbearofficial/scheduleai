using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace AiHelper.Client.Models;

public class AgentRequestModel
{
    [JsonPropertyName("messages")]
    [JsonProperty("messages")]
    public required MessageModel[] Messages { get; init; }
}