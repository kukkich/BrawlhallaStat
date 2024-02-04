namespace ReplayWatcher.Desktop.Model.Authentication.Storage;

public interface ITokenStorage
{
    public Task<string?> GetToken();
    public Task SaveToken(string token);
}