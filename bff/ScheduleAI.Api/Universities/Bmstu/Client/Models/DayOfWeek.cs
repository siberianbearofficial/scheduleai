using Newtonsoft.Json;

namespace BmstuSchedule.Client.Models;

public enum DayOfWeek
{
    [JsonProperty("monday")] Monday,
    [JsonProperty("tuesday")] Tuesday,
    [JsonProperty("wednesday")] Wednesday,
    [JsonProperty("thursday")] Thursday,
    [JsonProperty("friday")] Friday,
    [JsonProperty("saturday")] Saturday,
    [JsonProperty("sunday")] Sunday
}