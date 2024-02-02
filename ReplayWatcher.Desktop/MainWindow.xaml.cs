using Hardcodet.Wpf.TaskbarNotification;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using ReplayWatcher.Desktop.WindowComponents.Commands;
using ReplayWatcher.Desktop.ViewModel;

namespace ReplayWatcher.Desktop;

public partial class MainWindow
{
    private bool Hidden => !ShowInTaskbar;
    private readonly List<IDisposable> _disposeRequired = new();
    private readonly TaskbarIcon _taskbar;

    private bool _isPathInitialized = false;

    public MainWindow(IAppViewModel viewModel)
    {
        ViewModel = viewModel;

        _taskbar = CreateTaskbar();

        _disposeRequired.Add(_taskbar);

        InitializeComponent();
    }

    protected override async void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);

        await ViewModel!.StartApplicationCommand.Execute();
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
        var taskbar = new TaskbarIcon
        {
            Icon = System.Drawing.SystemIcons.Application,
            ToolTipText = "Replay Watcher",
            DoubleClickCommand = new ActionCommand(ShowWindow),
            ContextMenu = new ContextMenu
            {
                ItemsSource = new MenuItem[]
                {
                    new() { Header = "Open", Command = new ActionCommand(ShowWindow) },
                    new() { Header = "Exit", Command = new ActionCommand(ShutDown) }
                }
            }
        };

        return taskbar;
    }

    private void Dispose()
    {
        foreach (var disposable in _disposeRequired)
        {
            disposable.Dispose();
        }
    }
}
