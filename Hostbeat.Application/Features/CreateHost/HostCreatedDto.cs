using System;

namespace Hostbeat.Application.Features.CreateHost;

public class HostCreatedDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public bool Alerts { get; set; }
    public ushort MaxInactivityMinutes { get; set; }
}