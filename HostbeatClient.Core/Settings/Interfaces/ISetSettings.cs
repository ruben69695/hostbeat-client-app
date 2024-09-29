using System.Threading.Tasks;

namespace HostbeatClient.Core.Settings.Interfaces;

public interface ISetSettings
{
    Task SetAsync(Settings settings);
}