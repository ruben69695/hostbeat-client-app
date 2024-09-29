using System.Threading;
using System.Threading.Tasks;
using Hostbeat.Application.Interfaces.Services;
using Hostbeat.Utils;
using MediatR;

namespace Hostbeat.Application.Features.CreateHost;

/// <summary>
/// Create host command.
/// </summary>
/// <param name="name">Host name.</param>
/// <param name="alerts">True to enable host alerts otherwise False.</param>
public class CreateHostCommand(string name, bool alerts) : IRequest<Result<HostCreatedDto?>>
{
    /// <summary>
    /// Host name.
    /// </summary>
    /// <example>Linux Home Server</example>
    public string Name { get; set; } = name;

    /// <summary>
    /// true to enable, false to disable receiving alerts for this host.
    /// </summary>
    /// <example>true</example>
    public bool Alerts { get; set; } = alerts;
}

/// <summary>
/// Create host command handler.
/// </summary>
/// <param name="hostsService"></param>
public class CreateHostCommandHandler(IHostsService hostsService) : IRequestHandler<CreateHostCommand, Result<HostCreatedDto?>>
{
    public async Task<Result<HostCreatedDto?>> Handle(CreateHostCommand request, CancellationToken cancellationToken)
    {
        return await hostsService.CreateHost(request.Name, request.Alerts);
    }
}