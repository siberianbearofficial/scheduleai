using System.Text.Json.Serialization;

namespace Bmstu.Mock.Models;

public class DataModel<T>
{
    [JsonPropertyName("data")] public required T Data { get; init; }
}