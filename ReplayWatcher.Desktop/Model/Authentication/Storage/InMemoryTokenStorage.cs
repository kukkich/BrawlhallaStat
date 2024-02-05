using System.IO;
using System.Security.Cryptography;
using System.Text;
using ReplayWatcher.Desktop.Configuration;

namespace ReplayWatcher.Desktop.Model.Authentication.Storage;

public class InMemoryTokenStorage : ITokenStorage
{
    private readonly AppConfiguration _configuration;
    private string? _accessToken;

    public InMemoryTokenStorage(AppConfiguration configuration)
    {
        _configuration = configuration;
    }

    public Task<string?> GetAccessToken()
    {
        return Task.FromResult(_accessToken);
    }

    public Task SaveAccessToken(string token)
    {
        _accessToken = token;

        return Task.CompletedTask;
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

        return Encoding.UTF8.GetString(decryptedTokenBytes);
    }

    public async Task SaveRefreshToken(string token)
    {
        if (!File.Exists(_configuration.CookieFilePath))
        {
            File.Create(_configuration.CookieFilePath);
        }

        await File.WriteAllBytesAsync(_configuration.CookieFilePath, EncryptString(token));
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