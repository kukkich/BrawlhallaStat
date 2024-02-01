using Hardcodet.Wpf.TaskbarNotification;
using ReplayWatcher.Desktop.Watcher;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using ReplayWatcher.Desktop.WindowComponents.Commands;
using ReplayWatcher.Desktop.Configuration;
using Microsoft.Extensions.Logging;

namespace ReplayWatcher.Desktop;

public partial class MainWindow : Window
{
    private bool Hidden => !ShowInTaskbar;
    private readonly List<IDisposable> _disposeRequired = new ();
    private readonly ReplayWatcherService _replayWatcher;
    private readonly TaskbarIcon _taskbar;
    private readonly ConfigurationManager _configurationManager;

    private bool _isPathInitialized = false;

    public MainWindow(ReplayWatcherService replayWatcher, ConfigurationManager configurationManager)
    {
        _replayWatcher = replayWatcher;
        _configurationManager = configurationManager;

        _taskbar = CreateTaskbar();

        _disposeRequired.Add(_taskbar);
        _disposeRequired.Add(replayWatcher);

        InitializeComponent();
    }

    protected override void OnInitialized(EventArgs e)
    {
        try
        {
            _replayWatcher.Start();
            _isPathInitialized = true;
        }
        catch (InvalidOperationException)
        {
            _isPathInitialized = false;
        }

        CreateContextMenu();
        base.OnInitialized(e);
    }
    
    private void ShowWindow()
    {
        ShowInTaskbar = true;
        Show();
        WindowState = WindowState.Normal;
    }

    private void HideWindow()
    {
        ShowInTaskbar = false;
        Hide();
        WindowState = WindowState.Minimized;
    }

    private void ShutDown()
    {
        _taskbar.Dispose();
        Application.Current.Shutdown();
    }

    protected override void OnClosing(CancelEventArgs e)
    {
        if (!Hidden)
        {
            e.Cancel = true;
            HideWindow();
            return;
        }
        Dispose();
        base.OnClosing(e);
    }

    private TaskbarIcon CreateTaskbar()
    {
        var taskbar = new TaskbarIcon();
        taskbar.Icon = System.Drawing.SystemIcons.Application;
        taskbar.ToolTipText = "Replay Watcher";
        taskbar.DoubleClickCommand = new ActionCommand(ShowWindow);

        return taskbar;
    }

    private void CreateContextMenu()
    {
        _taskbar.ContextMenu = new ContextMenu
        {
            ItemsSource = new MenuItem[]
            {
                new() { Header = "Open", Command = new ActionCommand(ShowWindow) },
                new() { Header = "Exit", Command = new ActionCommand(ShutDown) }
            }
        };
    }

    private void Dispose()
    {
        foreach (var disposable in _disposeRequired)
        {
            disposable.Dispose();
        }
    }
}
