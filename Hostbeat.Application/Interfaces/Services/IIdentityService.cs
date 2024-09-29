using System.Threading.Tasks;
using Hostbeat.Application.Features.Login;
using Hostbeat.Utils;

namespace Hostbeat.Application.Interfaces.Services;

public interface IIdentityService
{
    /// <summary>
    /// Method to log in with an account against the Hostbeat API.
    /// </summary>
    /// <param name="email">User email.</param>
    /// <param name="password">User password.</param>
    /// <returns></returns>
    Task<Result<LoginDto?>> Login(string email, string password);
}