using System.Net;
using System.Net.Http;
using System.Text;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ReplayWatcher.Desktop.Model.Authentication.Storage;

namespace ReplayWatcher.Desktop.Model.Authentication.Services;

public class AuthenticationService : IAuthService
{
    private readonly ILogger<AuthenticationService> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly ITokenStorage _tokenStorage;
    private readonly CookieContainer _cookieContainer;

    private const string ApiClientName = "GeneralApiClient";
    private const string RefreshTokenCookieKey = "refreshToken";

    public AuthenticationService(
        ILogger<AuthenticationService> logger,
        IHttpClientFactory httpClientFactory,
        ITokenStorage tokenStorage,
        CookieContainer cookieContainer)
    {
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _tokenStorage = tokenStorage;
        _cookieContainer = cookieContainer;
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
        _tokenStorage.SaveAccessToken(accessToken);

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
        _tokenStorage.SaveAccessToken(accessToken);

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
        _tokenStorage.SaveAccessToken(accessToken);

        var baseAddress = new Uri(httpClient.BaseAddress.GetLeftPart(UriPartial.Authority)); // Получение базового адреса запроса (схема + хост)
        var cookies = _cookieContainer.GetCookies(baseAddress); // Получение кук для базового адреса
        var refreshTokenCookie = cookies[RefreshTokenCookieKey];
        if (refreshTokenCookie != null)
        {
            string refreshToken = refreshTokenCookie.Value;
            await _tokenStorage.SaveRefreshToken(refreshToken);
            _logger.LogDebug($"RefreshToken extracted: {refreshToken}");
        }
        else
        {
            _logger.LogWarning("RefreshToken cookie not found");
            return new AuthenticationResult(false, new() { "RefreshToken cookie not found" });
        }

        _logger.LogDebug("Refresh succeed");
        return new AuthenticationResult(true, null);
    }


    public Task Logout()
    {
        throw new NotImplementedException();
    }
}