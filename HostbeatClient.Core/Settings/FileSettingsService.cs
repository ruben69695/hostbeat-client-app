using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace HostbeatClient.Core.Settings;

public class SettingsFileService : BaseSettingsService
{
    private const string BaseFolderName = "Hostbeat";
    private const string FileName = "settings.json";
    
    private readonly string _basePath;
    private readonly string _fullPath;
    private readonly Encoding _defaultEncoding;

    public SettingsFileService()
    {
        _basePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData, Environment.SpecialFolderOption.Create), BaseFolderName);
        _fullPath = Path.Combine(_basePath, FileName);
        _defaultEncoding = Encoding.UTF8;
    }

    public override async Task<Settings> GetAsync(bool reloadCache = false)
    {
        if (reloadCache)
        {
            await ReadSettingsAsync();
        }

        return Settings!;
    }

    public override async Task SetAsync(Settings settings)
    {
        UpdateSettings(settings);
        await WriteSettingsAsync();
    }

    private async Task ReadSettingsAsync()
    {
        if (!Directory.Exists(_basePath))
        {
            Directory.CreateDirectory(_basePath);
            await File.WriteAllTextAsync(_fullPath, Settings.Default().ToJson(), _defaultEncoding);
        }

        string content = await File.ReadAllTextAsync(_fullPath);
		
        UpdateSettings(Settings.LoadFromJson(content));
    }

    private async Task WriteSettingsAsync()
    {
        if (Settings is null)
        {
            throw new InvalidOperationException("Configuration is null, read the settings first");
        }

        if (!Directory.Exists(_basePath))
        {
            Directory.CreateDirectory(_basePath);
        }

        await File.WriteAllTextAsync(_fullPath, Settings.ToJson(), _defaultEncoding);
    }
}