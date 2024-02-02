using ReplayWatcher.Desktop.Configuration;
using ReplayWatcher.Desktop.Model.Watcher;

namespace ReplayWatcher.Desktop.ViewModel;

public class AppViewModel : IAppViewModel
{
    public bool IsPathInitialized { get; set; }

    private readonly ReplayWatcherService _replayWatcher;
    private readonly ConfigurationManager _configurationManager;

    public AppViewModel(ReplayWatcherService replayWatcher, ConfigurationManager configurationManager)
    {
        _replayWatcher = replayWatcher;
        _configurationManager = configurationManager;
    }

    public Task StartApplication()
    {
        try
        {
            _replayWatcher.Start();
            IsPathInitialized = true;
        }
        catch (InvalidOperationException)
        {
            IsPathInitialized = false;
        }
    }
}