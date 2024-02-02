using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;

namespace ReplayWatcher.Desktop.Model.Authentication;

public class JwtDelegatingHandler : DelegatingHandler
{
    private TokenPair _tokens;
    private readonly IAuthService _authService;

    public JwtDelegatingHandler(IAuthService authService)
    {
        _authService = authService;
    }

    protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(_tokens.Access))
        {
            var loginResult = await _authService.Login(new LoginRequest(null, null));
            _tokens = loginResult.Tokens;
        }
        
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokens.Access);
        var response = await base.SendAsync(request, cancellationToken);

        if (response.StatusCode != HttpStatusCode.Unauthorized)
        {
            return response;
        }
        
        var refreshResult = await _authService.RefreshToken();
        _tokens = refreshResult.Tokens;
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", _tokens.Access);
        
        response = await base.SendAsync(request, cancellationToken);

        return response;
    }
}