using System.Text.Json.Serialization;

namespace BmstuSchedule.Client.Models;

public class HTTPValidationError
{
    [JsonPropertyName("detail")] public ValidationError[]? Detail { get; init; }
}