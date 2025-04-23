using System.Text.Json.Serialization;

namespace Bmstu.Mock.Models;

public class Audience
{
    [JsonPropertyName("uuid")] public Guid? Id { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
}