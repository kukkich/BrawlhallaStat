using System.IO;
using Microsoft.Extensions.Logging;
using ReplayWatcher.Desktop.Configuration;

namespace ReplayWatcher.Desktop.Model.Watcher;

public class ReplayWatcherService : IDisposable
{
    public string Path
    {
        get => _watcher.Path;
        set
        {
            _logger.LogInformation("Path changing from \"{OldPath}\" \n   to \"{NewPath}\"", _watcher.Path, value);
            _watcher.Path = value;
        }
    }

    public bool IsWatching => _watcher.EnableRaisingEvents;

    private readonly ConfigurationManager _configurationManager;
    private readonly ILogger<ReplayWatcherService> _logger;
    private readonly FileSystemWatcher _watcher;
    private readonly List<FileSystemEventHandler> _actions = new();

    public ReplayWatcherService(ConfigurationManager configurationManager, ILogger<ReplayWatcherService> logger)
    {
        _watcher = new FileSystemWatcher();

        _configurationManager = configurationManager;
        _logger = logger;
        Path = configurationManager.Configuration.WatcherDirectory;
        configurationManager.OnConfigurationChanged += OnPathChanged;
    }

    public void Start()
    {
        if (string.IsNullOrEmpty(Path) || !File.Exists(Path))
        {
            throw new InvalidOperationException("Directory for watching is empty");
        }
        _watcher.EnableRaisingEvents = true;

        _logger.LogInformation("Start watching");
    }

    public void Stop()
    {
        _watcher.EnableRaisingEvents = false;
        _logger.LogInformation("Stop watching");
    }

    public void BindAction(FileSystemEventHandler action)
    {
        _actions.Add(action);
        _watcher.Created += action;
    }

    private void OnPathChanged(AppConfiguration configuration)
    {
        Path = configuration.WatcherDirectory;
    }

    public void Dispose()
    {
        _configurationManager.OnConfigurationChanged -= OnPathChanged;
        foreach (var action in _actions)
        {
            _watcher.Created -= action;
        }
        _watcher.Dispose();
    }
}