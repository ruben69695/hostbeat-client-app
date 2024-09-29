namespace Hostbeat.Services.Features.CreateHost;

public class CreateHostRequest
{
    public string Name { get; set; } = null!;
    public bool Alerts { get; set; }
    public ushort MaxInactivityMinutes { get; private set; } = 3;
}