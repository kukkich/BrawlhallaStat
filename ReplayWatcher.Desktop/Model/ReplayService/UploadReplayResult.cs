namespace ReplayWatcher.Desktop.Model.ReplayService;

public record UploadReplayResult(bool IsSucceed, List<string>? Errors) 
    : RequestResult(IsSucceed, Errors);