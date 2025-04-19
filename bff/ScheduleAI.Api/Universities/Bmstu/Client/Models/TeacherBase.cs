using Newtonsoft.Json;

namespace BmstuSchedule.Client.Models;

public class TeacherBase
{
    [JsonProperty("id")] public required int Id { get; init; }
    [JsonProperty("first_name")] public required string FirstName { get; init; }
    [JsonProperty("middle_name")] public required string MiddleName { get; init; }
    [JsonProperty("last_name")] public required string LastName { get; init; }
    [JsonProperty("departments")] public required string[] Departments { get; init; }
}