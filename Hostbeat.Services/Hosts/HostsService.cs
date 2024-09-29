using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Hostbeat.Application.Features.CreateHost;
using Hostbeat.Application.Features.GetHostList;
using Hostbeat.Application.Interfaces.Services;
using Hostbeat.Utils;

namespace Hostbeat.Services.Hosts;

/// <summary>
/// Service to perform operation against the hosts resource of the Hostbeat HTTP API.
/// </summary>
public class HostsService : BaseService, IHostsService
{
    private const string ResourceUri = "api/hosts";
    private const ushort MinApiVersion = 1;
    private const ushort MaxApiVersion = 2;

    protected HostsService(string baseAddress, string? userToken) : base(baseAddress, userToken)
    {
    }

    /// <inheritdoc />
    public async Task<Result<HostCreatedDto?>> CreateHost(string name, bool alertsEnabled)
    {
        using var httpClient = CreateBaseClient();

        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, ResourceUri);
            httpRequest.Headers.Add("Authorization", $"Bearer {UserToken}");

            var httpResponse = await httpClient.SendAsync(httpRequest);

            switch (httpResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                {
                    var dataStream = await httpResponse.Content.ReadAsStreamAsync();
                    var data = await JsonSerializer.DeserializeAsync<HostCreatedDto>(dataStream);

                    return Result<HostCreatedDto?>.Success(data!);
                }
                case HttpStatusCode.Unauthorized: return Result<HostCreatedDto>.Failure(Error.Unauthorized);
                default: return Result<HostCreatedDto?>.Failure(Error.Unknown);
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e.ToString());
            return Result<HostCreatedDto?>.Failure(Error.Connectivity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return Result<HostCreatedDto?>.Failure(Error.Unknown);
        }
    }

    /// <inheritdoc />
    public async Task<Result<GetHostListDto?>> GetHosts()
    {
        using var httpClient = CreateBaseClient();

        try
        {
            var httpRequest = new HttpRequestMessage(HttpMethod.Post, ResourceUri);
            httpRequest.Headers.Add("Authorization", $"Bearer {UserToken}");
            httpRequest.Headers.Add("X-Api-Version", MaxApiVersion.ToString());

            var httpResponse = await httpClient.SendAsync(httpRequest);

            switch (httpResponse.StatusCode)
            {
                case HttpStatusCode.OK:
                {
                    var dataStream = await httpResponse.Content.ReadAsStreamAsync();
                    var data = await JsonSerializer.DeserializeAsync<List<HostItemDto>>(dataStream);

                    return Result<GetHostListDto?>.Success(GetHostListDto.CreateWithList(data!));
                }
                case HttpStatusCode.Unauthorized: return Result<GetHostListDto?>.Failure(Error.Unauthorized);
                default: return Result<GetHostListDto?>.Failure(Error.Unknown);
            }
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine(e.ToString());
            return Result<GetHostListDto?>.Failure(Error.Connectivity);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
            return Result<GetHostListDto?>.Failure(Error.Unknown);
        }
    }
}