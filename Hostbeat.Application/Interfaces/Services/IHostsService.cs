using System.Threading.Tasks;
using Hostbeat.Application.Features.CreateHost;
using Hostbeat.Application.Features.GetHostList;
using Hostbeat.Utils;

namespace Hostbeat.Application.Interfaces.Services;

public interface IHostsService
{
    /// <summary>
    /// Method to create a host with the current logged user.
    /// </summary>
    /// <param name="name">The host name we want to set.</param>
    /// <param name="alertsEnabled">True to set to receive host alerts using push notifications, otherwise False.</param>
    /// <returns></returns>
    Task<Result<HostCreatedDto?>> CreateHost(string name, bool alertsEnabled);
    
    /// <summary>
    /// Gets all available hosts for the current logged user.
    /// </summary>
    /// <returns>An instance of <see cref="GetHostListDto"/> that contains the list of hosts.</returns>
    Task<Result<GetHostListDto?>> GetHosts();
}