namespace ReplayWatcher.Desktop.Model.Authentication.Storage;

public interface ITokenStorage
{
    public string? GetAccessToken();
    public void SaveAccessToken(string token);
    public void RemoveAccessToken();

    public Task<string?> GetRefreshToken();
    public Task SaveRefreshToken(string token);
    public void RemoveRefreshToken();
}