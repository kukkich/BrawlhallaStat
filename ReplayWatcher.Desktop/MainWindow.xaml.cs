using Hardcodet.Wpf.TaskbarNotification;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace ReplayWatcher.Desktop;

public partial class MainWindow : Window
{
    private readonly TaskbarIcon _taskbar;
    private bool Hidden => !ShowInTaskbar;

    public MainWindow()
    {
        _taskbar = CreateTaskbar();
        InitializeComponent();
    }

    protected override void OnInitialized(EventArgs e)
    {
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
        _taskbar.Dispose();
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
}
