using System.Net;
using System.Net.Http;
using System.Text;
using BrawlhallaStat.Api.Contracts.Identity.Authentication;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using ReplayWatcher.Desktop.Model.Authentication.Storage;
// ReSharper disable ConditionIsAlwaysTrueOrFalseAccordingToNullableAPIContract
#pragma warning disable CS8604

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
        CookieContainer cookieContainer
        )
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

        var httpResponse = await httpClient.SendAsync(httpRequest);

        if (!httpResponse.IsSuccessStatusCode)
        {
            var error = await httpResponse.Content.ReadAsStringAsync();
            _logger.LogDebug("Error while logging: {error}", error);

            return new AuthenticationResult(false, new() { error });
        }

        var responseContentString = await httpResponse.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<LoginResultDto>(responseContentString)!;
        _tokenStorage.SaveAccessToken(response.AccessToken);

        if (!await SaveRefreshToken(httpClient))
        {
            return new AuthenticationResult(false, new() { "RefreshToken cookie not found" });
        }

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

        var httpResponse = await httpClient.SendAsync(httpRequest);

        if (!httpResponse.IsSuccessStatusCode)
        {
            var error = await httpResponse.Content.ReadAsStringAsync();
            _logger.LogDebug("Error while register: {error}", error);

            return new AuthenticationResult(false, new() { error });
        }

        var responseContentString = await httpResponse.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<LoginResultDto>(responseContentString)!;
        _tokenStorage.SaveAccessToken(response.AccessToken);

        if (!await SaveRefreshToken(httpClient))
        {
            return new AuthenticationResult(false, new() { "RefreshToken cookie not found" });
        }

        _logger.LogDebug("Register succeed");
        return new AuthenticationResult(true, null);
    }

    public async Task<AuthenticationResult> RefreshToken()
    {
        _logger.LogDebug("Refresh begin");

        var httpClient = _httpClientFactory.CreateClient(ApiClientName);

        if (!await TryLoadRefreshTokenIfNotExist(httpClient))
        {
            return new AuthenticationResult(false, new() { "RefreshToken not found in cookie and storage" });
        }

        var httpRequest = new HttpRequestMessage(HttpMethod.Post, "api/Auth/Refresh");
        var httpResponse = await httpClient.SendAsync(httpRequest);

        if (!httpResponse.IsSuccessStatusCode)
        {
            var error = await httpResponse.Content.ReadAsStringAsync();
            _logger.LogDebug("Error while refresh: {error}", error);

            return new AuthenticationResult(false, new() { error });
        }

        //todo refactor: code duplication in all methods
        var responseContentString = await httpResponse.Content.ReadAsStringAsync();
        var response = JsonConvert.DeserializeObject<LoginResultDto>(responseContentString)!;
        _tokenStorage.SaveAccessToken(response.AccessToken);


        if (!await SaveRefreshToken(httpClient))
        {
            return new AuthenticationResult(false, new() { "RefreshToken cookie not found" });
        }

        _logger.LogDebug("Refresh succeed");
        return new AuthenticationResult(true, null);
    }

    private async Task<bool> SaveRefreshToken(HttpClient httpClient)
    {
        var baseAddress = new Uri(httpClient.BaseAddress!.GetLeftPart(UriPartial.Authority));
        var cookies = _cookieContainer.GetCookies(baseAddress);
        var setByServerRefreshTokenCookie = cookies[RefreshTokenCookieKey];
        
        cookies.Remove(setByServerRefreshTokenCookie);
        var clientSetCookie = cookies[RefreshTokenCookieKey];

        if (clientSetCookie is not null)
        {
            clientSetCookie.Expired = true;
        }

        if (setByServerRefreshTokenCookie is not null)
        {
            _cookieContainer.SetCookies(baseAddress, $"refreshToken={setByServerRefreshTokenCookie.Value}");

            var refreshToken = setByServerRefreshTokenCookie.Value;
            await _tokenStorage.SaveRefreshToken(refreshToken);
            _logger.LogDebug("RefreshToken extracted: {refreshToken}", refreshToken);
        }
        else
        {
            _logger.LogWarning("RefreshToken cookie not found");
            {
                return false;
            }
        }

        return true;
    }

    private async Task<bool> TryLoadRefreshTokenIfNotExist(HttpClient httpClient)
    {
        var baseAddress = new Uri(httpClient.BaseAddress!.GetLeftPart(UriPartial.Authority));
        var cookies = _cookieContainer.GetCookies(baseAddress);

        var refreshTokenCookie = cookies[RefreshTokenCookieKey];

        if (refreshTokenCookie is not null)
        {
            return true;
        }

        var refreshTokenFromStorage = await _tokenStorage.GetRefreshToken();
        if (refreshTokenFromStorage is not null)
        {
            var cookie = new Cookie(RefreshTokenCookieKey, refreshTokenFromStorage)
            {
                Domain = baseAddress.Host,
                HttpOnly = true,
                Secure = true,
            };
            _cookieContainer.Add(cookie);
            _logger.LogDebug("RefreshToken loaded from storage and added to cookies: {token}", refreshTokenFromStorage);
            return true;
        }
        _logger.LogDebug("RefreshToken not found in cookie and storage");

        return false;
    }

    public Task Logout()
    {
        throw new NotImplementedException();
    }
}