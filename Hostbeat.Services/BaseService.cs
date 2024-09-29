using System;
using System.IO;
using System.Net.Http;

namespace Hostbeat.Services;

/// <summary>
/// Base service to inherit in every class to access to the Hostbeat API.
/// </summary>
public class BaseService
{
    private string _baseAddress;
    private TimeSpan _baseTimeout;
    private string? _userToken;

    public string UserToken => _userToken ?? string.Empty;

    protected BaseService(string baseAddress, string? userToken)
    {
        _baseAddress = baseAddress;
        _baseTimeout = TimeSpan.FromSeconds(15);
        _userToken = userToken;
    }

    /// <summary>
    /// Creates a base http client to consume.
    /// </summary>
    /// <returns></returns>
    protected HttpClient CreateBaseClient()
    {
        return new HttpClient
        {
            BaseAddress = new Uri(Path.Combine(_baseAddress)),
            Timeout = TimeSpan.FromSeconds(15)
        };
    }
    
    /// <summary>
    /// Method to invoke when we want to update the settings.
    /// </summary>
    /// <param name="baseAddress">Base address to set.</param>
    /// <param name="userToken">User token to set.</param>
    /// <exception cref="NotImplementedException"></exception>
    private void UpdateBaseSettings(string baseAddress, string userToken)
    {
        _baseAddress = baseAddress;
        _userToken = userToken;
    }
}