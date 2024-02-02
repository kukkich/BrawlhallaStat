namespace ReplayWatcher.Desktop.Model.ReplayService;

public interface IReplayService
{
    public Task Upload(string filePath);
}