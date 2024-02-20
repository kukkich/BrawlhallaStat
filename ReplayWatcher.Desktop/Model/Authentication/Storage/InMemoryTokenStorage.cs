using System.IO;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Logging;
using ReplayWatcher.Desktop.Configuration;

namespace ReplayWatcher.Desktop.Model.Authentication.Storage;

public class InMemoryTokenStorage : ITokenStorage
{
    private readonly AppConfiguration _configuration;
    private readonly ILogger<InMemoryTokenStorage> _logger;
    private string? _accessToken;

    public InMemoryTokenStorage(AppConfiguration configuration, ILogger<InMemoryTokenStorage> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    public string? GetAccessToken()
    {
        return _accessToken;
    }

    public void SaveAccessToken(string token)
    {
        _accessToken = token;
    }

    public void RemoveAccessToken()
    {
        _accessToken = null;
    }

    public async Task<string?> GetRefreshToken()
    {
        if (!File.Exists(_configuration.CookieFilePath))
        {
            return null;
        }
        
        var encryptedToken = await File.ReadAllBytesAsync(_configuration.CookieFilePath);
        var decryptedTokenBytes = Decrypt(encryptedToken);
        var token = Encoding.UTF8.GetString(decryptedTokenBytes);
        _logger.LogDebug("Token extracted from file: {token}", token);
        return token;
    }

    public async Task SaveRefreshToken(string token)
    {
        if (!File.Exists(_configuration.CookieFilePath))
        {
            File.Create(_configuration.CookieFilePath);
            _logger.LogDebug("Cookie file created");
        }

        await File.WriteAllBytesAsync(_configuration.CookieFilePath, EncryptString(token));
        _logger.LogDebug("Refresh token saved in file: {token}", token);
    }

    public void RemoveRefreshToken()
    {
        File.Delete(_configuration.CookieFilePath);
    }

    private byte[] EncryptString(string data)
    {
        return ProtectedData.Protect(
            Encoding.UTF8.GetBytes(data),
            null,
            DataProtectionScope.CurrentUser
        );
    }

    private static byte[] Decrypt(byte[] data)
    {
        return ProtectedData.Unprotect(
            data,
            null,
            DataProtectionScope.CurrentUser
        );
    }
}