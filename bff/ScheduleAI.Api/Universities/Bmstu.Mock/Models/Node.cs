using System.Text.Json.Serialization;

namespace Bmstu.Mock.Models;

public class Node
{
    [JsonPropertyName("abbr")] public string? Abbr { get; set; }
    [JsonPropertyName("name")] public string? Name { get; set; }
    [JsonPropertyName("uuid")] public Guid? Id { get; set; }
    [JsonPropertyName("children")] public Node[] Children { get; set; } = [];
    [JsonPropertyName("nodeType")] public string? Type { get; set; }
}