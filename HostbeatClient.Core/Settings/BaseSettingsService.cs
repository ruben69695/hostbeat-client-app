using System;
using System.Threading.Tasks;
using HostbeatClient.Core.Settings.Events;
using HostbeatClient.Core.Settings.Interfaces;

namespace HostbeatClient.Core.Settings;

public abstract class BaseSettingsService : IGetSettings, ISetSettings
{
    /// <summary>
    /// Event invoked when settings changes.
    /// </summary>
    public event EventHandler<SettingsChangedEventArgs>? SettingsChanged;
    
    /// <summary>
    /// Settings property.
    /// </summary>
    public Settings? Settings { get; private set; }

    /// <summary>
    /// Setter method to set settings value.
    /// </summary>
    /// <param name="settings"></param>
    protected void UpdateSettings(Settings settings)
    {
        Settings = settings;
        SettingsChanged?.Invoke(this, SettingsChangedEventArgs.Create(settings));
    }

    public abstract Task<Settings> GetAsync(bool reloadCache = false);
    public abstract Task SetAsync(Settings settings);
}