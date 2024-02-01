using System;
using System.IO;
using Microsoft.Extensions.FileSystemGlobbing;
using ReplayWatcher.Desktop.Configuration;

namespace ReplayWatcher.Desktop.Watcher;

public class ReplayWatcherService : IDisposable
{
    public string Path
    {
        get => _watcher.Path;
        set => _watcher.Path = value;
    }
    public bool IsWatching => _watcher.EnableRaisingEvents;

    private readonly ConfigurationManager _configurationManager;
    private readonly FileSystemWatcher _watcher;
    private readonly List<FileSystemEventHandler> _actions = new();

    public ReplayWatcherService(ConfigurationManager configurationManager)
    {
        _watcher = new FileSystemWatcher();

        _configurationManager = configurationManager;
        Path = configurationManager.Configuration.WatcherDirectory;
        configurationManager.OnConfigurationChanged += OnPathChanged;
    }

    public void Start()
    {
        if (String.IsNullOrEmpty(Path) || !File.Exists(Path))
        {
            throw new InvalidOperationException("Directory for watching is empty");
        }
        _watcher.EnableRaisingEvents = true;
    }

    public void Stop()
    {
        _watcher.EnableRaisingEvents = false;
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