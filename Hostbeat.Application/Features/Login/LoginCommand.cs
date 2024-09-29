using System.Threading;
using System.Threading.Tasks;
using Hostbeat.Application.Interfaces.Services;
using Hostbeat.Utils;
using MediatR;

namespace Hostbeat.Application.Features.Login;

/// <summary>
/// Login command.
/// </summary>
/// <param name="email">User email.</param>
/// <param name="password">User password.</param>
public class LoginCommand(string email, string password) : IRequest<Result<LoginDto?>>
{
    /// <summary>
    /// Account email
    /// </summary>
    /// <example>ruben.arre6@gmail.com</example>
    public string Email { get; set; } = email;

    /// <summary>
    /// Account password
    /// </summary>
    /// <example>ruben123</example>
    public string Password { get; set; } = password;
}

/// <summary>
/// Login command handler.
/// </summary>
/// <param name="identityService"></param>
public class LoginCommandHandler(IIdentityService identityService) : IRequestHandler<LoginCommand, Result<LoginDto?>>
{
    public async Task<Result<LoginDto?>> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var loginResult = await identityService.Login(request.Email, request.Password);
        return loginResult;
    }
}