namespace ReplayWatcher.Desktop.Model.Authentication;

public record RegisterRequest(string Login, string NickName, string Email, string Password);