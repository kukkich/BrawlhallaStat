using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using ReplayWatcher.Desktop.Model.Authentication.Services;
using ReplayWatcher.Desktop.Model.Authentication.Storage;

namespace ReplayWatcher.Desktop.Model.Authentication;

public class JwtDelegatingHandler : DelegatingHandler
{
    private readonly IAuthService _authService;
    private readonly ITokenStorage _tokenStorage;

    public JwtDelegatingHandler(IAuthService authService, ITokenStorage tokenStorage)
    {
        _authService = authService;
        _tokenStorage = tokenStorage;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (request.RequestUri!.PathAndQuery.Contains("/api/Auth/Login") ||
            request.RequestUri.PathAndQuery.Contains("/api/Auth/Register"))
        {
            return await base.SendAsync(request, cancellationToken);
        }

        var token = await _tokenStorage.GetAccessToken();

        if (string.IsNullOrWhiteSpace(token))
        {
            return await base.SendAsync(request, cancellationToken);
        }

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", token);
        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode is not HttpStatusCode.Unauthorized)
        {
            return response;
        }
        
        var refreshResult = await _authService.RefreshToken();
        if (!refreshResult.IsSucceed)
        {
            return response;
        }
        var newToken = await _tokenStorage.GetAccessToken();

        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", newToken);

        return await base.SendAsync(request, cancellationToken);
    }
}