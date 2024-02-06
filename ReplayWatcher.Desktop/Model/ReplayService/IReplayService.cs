namespace ReplayWatcher.Desktop.Model.ReplayService;

public interface IReplayService
{
    public Task<UploadReplayResult> Upload(string filePath);
}