using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using HostbeatClient.Core.Enums;

namespace HostbeatClient.Core.Settings;

public class Settings
{
    [JsonIgnore]
    public string Url { get; private set; } = "https://hostbeatapi.rubenarrebola.pro";
    public string Token { get; set; }
    public double Interval { get; set; }
    public bool AutoStart {  get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public AppStartupValue AppStartupOnLogin { get; set; }

    public Settings(string token, double interval, bool autoStart, AppStartupValue appStartupOnLogin)
    {
        Token = token ?? throw new ArgumentNullException(nameof(token));
        Interval = interval;
        AutoStart = autoStart;
        AppStartupOnLogin = appStartupOnLogin;
    }

    public string ToJson()
    {
        return JsonSerializer.Serialize(this);
    }

    public static Settings LoadFromJson(string json)
    {
        return JsonSerializer.Deserialize<Settings>(json)!;
    }

    public static Settings Default()
    {
        return new (string.Empty, 1.0d, false, AppStartupValue.Minimized);
    }
}