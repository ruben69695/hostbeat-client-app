using System;

namespace Hostbeat.Services.Features.CreateHost;

public class HostCreatedResponse
{
    public Guid Id { get; set; }
    public string Name { get; set; } = null!;
    public bool Alerts { get; set; }
    public ushort MaxInactivityMinutes { get; set; }
}