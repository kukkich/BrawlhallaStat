using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;

namespace ReplayWatcher.Desktop.Model.Authentication;

public class JwtDelegatingHandler : DelegatingHandler
{
    private string _token;
    private readonly IAuthService _authService;

    public JwtDelegatingHandler(IAuthService authService)
    {
        _authService = authService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_token))
        {
            _token = await _authService.GetToken();
        }
        
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);
        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode != HttpStatusCode.Unauthorized)
        {
            return response;
        }
        
        _token = await _authService.RefreshToken();
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _token);

        response = await base.SendAsync(request, cancellationToken);

        return response;
    }
}