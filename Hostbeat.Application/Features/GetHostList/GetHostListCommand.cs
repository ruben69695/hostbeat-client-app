using System.Threading;
using System.Threading.Tasks;
using Hostbeat.Application.Interfaces.Services;
using Hostbeat.Utils;
using MediatR;

namespace Hostbeat.Application.Features.GetHostList;

/// <summary>
/// Get host list command.
/// </summary>
public class GetHostListCommand : IRequest<Result<GetHostListDto?>>;

/// <summary>
/// Get host list command handler.
/// </summary>
public class GetHostListCommandHandler(IHostsService hostsService) : IRequestHandler<GetHostListCommand, Result<GetHostListDto?>>
{
    public async Task<Result<GetHostListDto?>> Handle(GetHostListCommand request, CancellationToken cancellationToken)
    {
        return await hostsService.GetHosts();
    }
}