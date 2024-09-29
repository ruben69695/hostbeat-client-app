using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Hostbeat.Application.Features.Login;
using Hostbeat.Application.Interfaces.Services;
using Hostbeat.Services.Identity.Errors;
using Hostbeat.Utils;

namespace Hostbeat.Services.Identity;

/// <summary>
/// Service to perform operation against the identity resource of the Hostbeat HTTP API.
/// </summary>
public sealed class IdentityService(string baseAddress, string? userToken)
    : BaseService(baseAddress, userToken), IIdentityService
{
    private const string ResourceUri = "api/identity";

    /// <inheritdoc />
    public async Task<Result<LoginDto?>> Login(string email, string password)
    {
        using var httpClient = CreateBaseClient();

        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, $"{ResourceUri}/login");
            var httpResponse = await httpClient.SendAsync(httpRequest);

            switch (httpResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                {
                    var dataStream = await httpResponse.Content.ReadAsStreamAsync();
                    var data = await JsonSerializer.DeserializeAsync<LoginDto>(dataStream);

                    return Result<LoginDto?>.Success(data!);
                }
                case HttpStatusCode.BadRequest: return Result<LoginDto?>.Failure(LoginErrors.InvalidParameters);
                case HttpStatusCode.Forbidden: return Result<LoginDto?>.Failure(LoginErrors.InvalidCredentials);
                default: return Result<LoginDto?>.Failure(Error.Unknown);
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e.ToString());
            return Result<LoginDto?>.Failure(Error.Connectivity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return Result<LoginDto?>.Failure(Error.Unknown);
        }
    }
}