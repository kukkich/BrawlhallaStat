using Microsoft.Extensions.Logging;

namespace ReplayWatcher.Desktop.Model.Authentication.Services;

public class LoggerAuthService : IAuthService
{
    private readonly ILogger<LoggerAuthService> _logger;

    public LoggerAuthService(ILogger<LoggerAuthService> logger)
    {
        _logger = logger;
    }

    public Task<AuthenticationResult> Login(LoginRequest request)
    {
        _logger.LogInformation("Login");
        return Task.FromResult(new AuthenticationResult(true, null));
    }

    public Task<AuthenticationResult> Register(RegisterRequest request)
    {
        _logger.LogInformation("Register");
        return Task.FromResult(new AuthenticationResult(true, null));
    }

    public Task<AuthenticationResult> RefreshToken()
    {
        _logger.LogInformation("RefreshToken");
        return Task.FromResult(new AuthenticationResult(true, null));
    }

    public Task Logout()
    {
        _logger.LogInformation("Logout");
        return Task.CompletedTask;
    }
}