using Newtonsoft.Json;

namespace BmstuSchedule.Client.Models;

public class HTTPValidationError
{
    [JsonProperty("detail")] public ValidationError[]? Detail { get; init; }
}