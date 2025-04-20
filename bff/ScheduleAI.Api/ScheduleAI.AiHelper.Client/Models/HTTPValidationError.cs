using Newtonsoft.Json;

namespace AiHelper.Client.Models;

public class HTTPValidationError
{
    [JsonProperty("detail")] public ValidationError[]? Detail { get; init; }
}