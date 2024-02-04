namespace ReplayWatcher.Desktop.Model.Authentication.Storage;

public class InMemoryTokenStorage : ITokenStorage
{
    private string? _token;
    
    public Task<string?> GetToken()
    {
        return Task.FromResult(_token);
    }

    public Task SaveToken(string token)
    {
        _token = token;

        return Task.CompletedTask;
    }
}