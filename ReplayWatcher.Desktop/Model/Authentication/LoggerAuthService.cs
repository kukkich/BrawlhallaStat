using Microsoft.Extensions.Logging;

namespace ReplayWatcher.Desktop.Model.Authentication;

public class LoggerAuthService : IAuthService
{
    private readonly ILogger<LoggerAuthService> _logger;

    public LoggerAuthService(ILogger<LoggerAuthService> logger)
    {
        _logger = logger;
    }

    public Task<string> GetToken()
    {
        _logger.LogInformation("Got JWT");

        return Task.FromResult("jwt token");
    }

    public Task<string> RefreshToken()
    {
        _logger.LogInformation("Refreshed JWT");

        return Task.FromResult("refreshed jwt token");
    }
}