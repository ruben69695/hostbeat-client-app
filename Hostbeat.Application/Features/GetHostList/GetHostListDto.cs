using System;
using System.Collections.Generic;
using Hostbeat.Application.Enums;

namespace Hostbeat.Application.Features.GetHostList;

public class GetHostListDto
{
    public List<HostItemDto> Data { get; set; }

    private GetHostListDto(List<HostItemDto> list)
    {
        Data = list;
    }

    public static GetHostListDto CreateWithList(List<HostItemDto> list)
    {
        return new GetHostListDto(list);
    }
}

public class HostItemDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public bool Alerts { get; set; }
    public ushort MaxInactivityMinutes { get; set; }
    public HostStatusDto? Status { get; set; }
    public List<HostCpuCoresDto>? CpuCores { get; set; }
    public HostMemoryDto? Memory { get; set; }
}

public sealed class HostCpuCoresDto
{
    public int CoreId { get; set; }
    public float Usage { get; set; }
}

public sealed class HostMemoryDto
{
    public ulong Total { get; set; }
    public ulong Used { get; set; }
}


public sealed class HostStatusDto
{
    public HostStatusValue Value { get; set; }
    public DateTime UpdatedAt { get; set; }
}