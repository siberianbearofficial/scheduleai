using System.Text.Json.Serialization;

namespace BmstuSchedule.Client.Models;

public enum DayOfWeek
{
    [JsonPropertyName("monday")] Monday,
    [JsonPropertyName("tuesday")] Tuesday,
    [JsonPropertyName("wednesday")] Wednesday,
    [JsonPropertyName("thursday")] Thursday,
    [JsonPropertyName("friday")] Friday,
    [JsonPropertyName("saturday")] Saturday,
    [JsonPropertyName("sunday")] Sunday
}