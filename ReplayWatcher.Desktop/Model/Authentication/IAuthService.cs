namespace ReplayWatcher.Desktop.Model.Authentication;

public interface IAuthService
{
    public Task<string> GetToken();
    public Task<string> RefreshToken();
}