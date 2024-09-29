using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Hostbeat.Services.Utils;

namespace Hostbeat.Services.Features.GetHostList;

public class GetHostListService : BaseService
{
    private const string RequestUri = "api/hosts";
    private const ushort ApiVersion = 2;

    protected GetHostListService(string baseAddress, string? userToken) : base(baseAddress, userToken)
    { }

    public async Task<Result<GetHostListResponse?>> Handle()
    {
        using var httpClient = CreateBaseClient();

        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, RequestUri);
            httpRequest.Headers.Add("Authorization", $"Bearer {UserToken}");
            httpRequest.Headers.Add("X-Api-Version", ApiVersion.ToString());
            
            var httpResponse = await httpClient.SendAsync(httpRequest);

            switch (httpResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                {
                    var dataStream = await httpResponse.Content.ReadAsStreamAsync();
                    var data = await JsonSerializer.DeserializeAsync<List<HostItemResponse>>(dataStream);

                    return Result<GetHostListResponse?>.Success(GetHostListResponse.CreateWithList(data!));
                }
                case HttpStatusCode.Unauthorized: return Result<GetHostListResponse?>.Failure(Error.Unauthorized);
                default: return Result<GetHostListResponse?>.Failure(Error.Unknown);
            }
        }
        catch (HttpRequestException e) 
        {
            Console.WriteLine(e.ToString());
            return Result<GetHostListResponse?>.Failure(Error.Connectivity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return Result<GetHostListResponse?>.Failure(Error.Unknown);
        }
    }
}