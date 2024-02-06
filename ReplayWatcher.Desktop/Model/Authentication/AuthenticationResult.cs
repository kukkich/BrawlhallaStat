namespace ReplayWatcher.Desktop.Model.Authentication;

public record AuthenticationResult(bool IsSucceed, List<string>? Errors) 
    : RequestResult(IsSucceed, Errors);