namespace Hostbeat.Application.Features.Login;

/// <summary>
/// Creates a login dto.
/// </summary>
/// <param name="userId">User id.</param>
/// <param name="token">Generated user token.</param>
/// <param name="refreshToken">Generated user refresh token.</param>
public struct LoginDto(string userId, string token, string refreshToken)
{
    public string UserId { get; private set; } = userId;
    public string Token { get; private set; } = token;
    public string RefreshToken { get; private set; } = refreshToken;
}