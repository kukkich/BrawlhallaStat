namespace ReplayWatcher.Desktop.Model;

public record RequestResult(bool IsSucceed, List<string>? Errors);