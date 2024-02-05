using Hardcodet.Wpf.TaskbarNotification;
using System.ComponentModel;
using System.Reactive.Linq;
using System.Windows;
using System.Windows.Controls;
using ReactiveUI;
using ReplayWatcher.Desktop.WindowComponents.Commands;
using ReplayWatcher.Desktop.ViewModel;
using System.Reactive.Disposables;
using ReplayWatcher.Desktop.ViewModel.Auth;

namespace ReplayWatcher.Desktop;

public partial class MainWindow
{
    private bool Hidden => !ShowInTaskbar;
    private readonly List<IDisposable> _disposeRequired = new();
    private readonly TaskbarIcon _taskbar;

    private bool _isPathInitialized = false;

    public MainWindow(AppViewModel viewModel)
    {
        ViewModel = viewModel;
        DataContext = ViewModel;

        _taskbar = CreateTaskbar();

        _disposeRequired.Add(_taskbar);
        InitializeComponent();
        this.WhenActivated(disposables =>
        {
            this.Bind(ViewModel, vm => vm.AuthContext.Login, view => view.LoginLoginTextBox.Text)
                .DisposeWith(disposables);
            this.Bind(ViewModel, vm => vm.AuthContext.Login, view => view.RegisterLoginTextBox.Text)
                .DisposeWith(disposables);
            this.Bind(ViewModel, vm => vm.AuthContext.Email, view => view.RegisterEmailTextBox.Text)
                .DisposeWith(disposables);
            

            LoginPasswordBox.Events().PasswordChanged
                .Subscribe(_ =>
                {
                    ViewModel.AuthContext.Password = LoginPasswordBox.Password;
                })
                .DisposeWith(disposables);
            RegisterPasswordBox.Events().PasswordChanged
                .Subscribe(_ =>
                {
                    ViewModel.AuthContext.Password = LoginPasswordBox.Password;
                })
                .DisposeWith(disposables);

            this.BindCommand(ViewModel, vm => vm.LoginCommand, view => view.LoginButton)
                .DisposeWith(disposables);
            this.BindCommand(ViewModel, vm => vm.RegisterCommand, view => view.RegisterButton)
                .DisposeWith(disposables);
            this.BindCommand(ViewModel, vm => vm.GetSecureDataCommand, view => view.GetDataButton)
                .DisposeWith(disposables);
        });
    }

    protected override async void OnInitialized(EventArgs e)
    {
        base.OnInitialized(e);
        //await ViewModel!.StartApplicationCommand.Execute();
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
