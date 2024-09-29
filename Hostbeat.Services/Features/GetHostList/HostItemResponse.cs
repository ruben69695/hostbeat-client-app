using System;
using System.Collections.Generic;
using Hostbeat.Services.Enums;

namespace Hostbeat.Services.Features.GetHostList;

public class HostItemResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public bool Alerts { get; set; }
    public ushort MaxInactivityMinutes { get; set; }
    public HostStatusDto? Status { get; set; }
    public List<HostCpuCoresDto>? CpuCores { get; set; }
    public HostMemoryDto? Memory { get; set; }
}

public sealed class HostStatusDto
{
    public HostStatusValue Value { get; set; }
    public DateTime UpdatedAt { get; set; }
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