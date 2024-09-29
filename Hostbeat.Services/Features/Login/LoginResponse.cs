namespace Hostbeat.Services.Features.Login;

public class LoginResponse
{
    public string UserId { get; set; } = null!;
    public string Token { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
}