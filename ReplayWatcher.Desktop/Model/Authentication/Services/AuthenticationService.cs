using System.Net.Http;
using System.Text;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ReplayWatcher.Desktop.Model.Authentication.Storage;

namespace ReplayWatcher.Desktop.Model.Authentication.Services;

public class AuthenticationService : IAuthService
{
    private readonly ILogger<AuthenticationService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ITokenStorage _tokenStorage;
    private const string ApiClientName = "GeneralApiClient";

    public AuthenticationService(
        ILogger<AuthenticationService> logger,
        IHttpClientFactory httpClientFactory,
        ITokenStorage tokenStorage)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _tokenStorage = tokenStorage;
    }

    public async Task<AuthenticationResult> Login(LoginRequest request)
    {
        _logger.LogDebug("Login begin");

        var httpClient = _httpClientFactory.CreateClient(ApiClientName);
        var jsonContent = JsonConvert.SerializeObject(request);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "api/Auth/Login")
        {
            Content = content
        };

        var response = await httpClient.SendAsync(httpRequest);

        if (!response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ErrorResult>(json)!;
            _logger.LogDebug("Error while logging");

            return new AuthenticationResult(false, new() { error.Text });
        }

        var accessToken = await response.Content.ReadAsStringAsync();
        await _tokenStorage.SaveToken(accessToken);

        _logger.LogDebug("Login succeed");
        return new AuthenticationResult(true, null);
    }

    public async Task<AuthenticationResult> Register(RegisterRequest request)
    {
        _logger.LogDebug("Register begin");

        var httpClient = _httpClientFactory.CreateClient(ApiClientName);
        var jsonContent = JsonConvert.SerializeObject(request);
        var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "api/Auth/Register")
        {
            Content = content
        };

        var response = await httpClient.SendAsync(httpRequest);

        if (!response.IsSuccessStatusCode)
        {
            var json = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ErrorResult>(json)!;
            _logger.LogDebug("Error while register");

            return new AuthenticationResult(false, new() { error.Text });
        }

        var accessToken = await response.Content.ReadAsStringAsync();
        await _tokenStorage.SaveToken(accessToken);

        _logger.LogDebug("Register succeed");
        return new AuthenticationResult(true, null);
    }

    public async Task<AuthenticationResult> RefreshToken()
    {
        _logger.LogDebug("Refresh begin");

        var httpClient = _httpClientFactory.CreateClient(ApiClientName);

        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "api/Auth/Refresh");

        var response = await httpClient.SendAsync(httpRequest);

        if (!response.IsSuccessStatusCode)
        {
            _logger.LogDebug("Error while refresh");
            var json = await response.Content.ReadAsStringAsync();
            var error = JsonConvert.DeserializeObject<ErrorResult>(json)!;

            return new AuthenticationResult(false, new() { error.Text });
        }

        var accessToken = await response.Content.ReadAsStringAsync();
        await _tokenStorage.SaveToken(accessToken);

        _logger.LogDebug("Refresh succeed");
        return new AuthenticationResult(true, null);
    }

    public Task Logout()
    {
        throw new NotImplementedException();
    }
}