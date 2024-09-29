using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Hostbeat.Services.Utils;

namespace Hostbeat.Services.Features.Login;

public sealed class LoginService(string baseAddress, string? userToken)
    : BaseService(baseAddress, userToken)
{
    private const string RequestUri = "api/identity/login";
    
    public async Task<Result<LoginResponse?>> Handle(LoginRequest request)
    {
        using var httpClient = CreateBaseClient();

        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, RequestUri);
            var httpResponse = await httpClient.SendAsync(httpRequest);

            switch (httpResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                {
                    var dataStream = await httpResponse.Content.ReadAsStreamAsync();
                    var data = await JsonSerializer.DeserializeAsync<LoginResponse>(dataStream);

                    return Result<LoginResponse?>.Success(data!);
                }
                case HttpStatusCode.BadRequest: return Result<LoginResponse>.Failure(LoginErrors.InvalidParameters);
                case HttpStatusCode.Forbidden: return Result<LoginResponse>.Failure(LoginErrors.InvalidCredentials);
                default: return Result<LoginResponse?>.Failure(Error.Unknown);
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e.ToString());
            return Result<LoginResponse?>.Failure(Error.Connectivity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return Result<LoginResponse?>.Failure(Error.Unknown);
        }
    }
}