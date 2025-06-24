using System.Text.Json.Serialization;

namespace BmstuSchedule.Client.Models;

public class TeacherBase
{
    [JsonPropertyName("id")] public required int Id { get; init; }
    [JsonPropertyName("first_name")] public required string FirstName { get; init; }
    [JsonPropertyName("middle_name")] public required string MiddleName { get; init; }
    [JsonPropertyName("last_name")] public required string LastName { get; init; }
    [JsonPropertyName("departments")] public string[]? Departments { get; init; }
}