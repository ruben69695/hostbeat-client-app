using System;

namespace HostbeatClient.Core.Settings.Events;

/// <summary>
/// Event args used when settings have changed. It has the new settings.
/// </summary>
public class SettingsChangedEventArgs : EventArgs
{
    /// <summary>
    /// New settings value.
    /// </summary>
    public Settings NewValue { get; private set; }
    
    private SettingsChangedEventArgs(Settings newValue)
    {
        NewValue = newValue;
    }

    /// <summary>
    /// Named constructor to create a new instance of type <see cref="SettingsChangedEventArgs" />
    /// </summary>
    /// <param name="settings"></param>
    /// <returns></returns>
    public static SettingsChangedEventArgs Create(Settings settings)
    {
        return new SettingsChangedEventArgs(settings);
    }
}