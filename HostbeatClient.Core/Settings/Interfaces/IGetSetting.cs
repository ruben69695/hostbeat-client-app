using System.Threading.Tasks;

namespace HostbeatClient.Core.Settings.Interfaces;

public interface IGetSettings
{
    Task<Settings> GetAsync(bool reloadCache = false);
}