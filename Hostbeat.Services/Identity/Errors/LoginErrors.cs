using Hostbeat.Utils;

namespace Hostbeat.Services.Identity.Errors;

public static class LoginErrors
{
    public static readonly Error InvalidParameters =
        new("Login.InvalidParameters", "Username or password are not in the correct format");

    public static readonly Error InvalidCredentials =
        new("Login.InvalidCredentials", "The email or the password are not correct");
}