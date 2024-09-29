using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Hostbeat.Services.Utils;

namespace Hostbeat.Services.Features.CreateHost;

public sealed class CreateHostService(string baseAddress, string? userToken)
    : BaseService(baseAddress, userToken)
{
    private const string RequestUri = "api/hosts";

    public async Task<Result<HostCreatedResponse?>> Handle(CreateHostRequest request)
    {
        using var httpClient = CreateBaseClient();

        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, RequestUri);
            httpRequest.Headers.Add("Authorization", $"Bearer {UserToken}");
            
            var httpResponse = await httpClient.SendAsync(httpRequest);

            switch (httpResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                {
                    var dataStream = await httpResponse.Content.ReadAsStreamAsync();
                    var data = await JsonSerializer.DeserializeAsync<HostCreatedResponse>(dataStream);

                    return Result<HostCreatedResponse?>.Success(data!);
                }
                case HttpStatusCode.Unauthorized: return Result<HostCreatedResponse>.Failure(Error.Unauthorized);
                default: return Result<HostCreatedResponse?>.Failure(Error.Unknown);
            }
        }
        catch (HttpRequestException e) 
        {
            Console.WriteLine(e.ToString());
            return Result<HostCreatedResponse?>.Failure(Error.Connectivity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return Result<HostCreatedResponse?>.Failure(Error.Unknown);
        }
    }
}