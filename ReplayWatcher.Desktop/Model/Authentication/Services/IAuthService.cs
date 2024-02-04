namespace ReplayWatcher.Desktop.Model.Authentication.Services;

public interface IAuthService
{
    public Task<AuthenticationResult> Login(LoginRequest request);
    public Task<AuthenticationResult> Register(RegisterRequest request);
    public Task<AuthenticationResult> RefreshToken();
    public Task Logout();
}