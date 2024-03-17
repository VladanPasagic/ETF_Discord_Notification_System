using Newtonsoft.Json;

namespace ETF_Discord_Notification_System;

public class Configuration
{
    [JsonProperty("webhook_url")]
    public string? WebhookUrl { get; set; }

    [JsonProperty("year_data")]
    public List<YearData>? Years { get; set; }

    [JsonProperty("timeout")]
    public int Timeout { get; set; }
}

public class YearData
{
    [JsonProperty("year")]
    public int Year { get; set; }

    [JsonProperty("notified")]
    public bool Notified { get; set; }

    [JsonProperty("role_id")]
    public ulong? RoleId { get; set; }

    [JsonProperty("thread_id")]
    public ulong? ThreadId { get; set; }

}
