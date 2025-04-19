using Newtonsoft.Json;

namespace BmstuSchedule.Client.Models;

public enum Week
{
    [JsonProperty("all")] All,
    [JsonProperty("odd")] Odd,
    [JsonProperty("even")] Even
}