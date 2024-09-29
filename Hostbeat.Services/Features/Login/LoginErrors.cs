using Hostbeat.Services.Utils;

namespace Hostbeat.Services.Features.Login;

public static class LoginErrors
{
    public static readonly Error InvalidParameters =
        new Error("Login.InvalidParameters", "Username or password are not in the correct format");

    public static readonly Error InvalidCredentials =
        new Error("Login.InvalidCredentials", "The email or the password are not correct");
}