using System.Text.Json.Serialization;

namespace Bmstu.Mock.Models;

public class Teacher
{
    [JsonPropertyName("uuid")] public Guid Id { get; set; }
    [JsonPropertyName("lastName")] public string? LastName { get; set; }
    [JsonPropertyName("firstName")] public string? FirstName { get; set; }
    [JsonPropertyName("middleName")] public string? MiddleName { get; set; }
}