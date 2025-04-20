using Newtonsoft.Json;

namespace AiHelper.Client.Models;

public class FunctionResultsModel
{
    [JsonProperty("messages")] public required MessageModel[] Messages { get; init; }
    [JsonProperty("function_result")] public required string FunctionResult { get; init; }
}