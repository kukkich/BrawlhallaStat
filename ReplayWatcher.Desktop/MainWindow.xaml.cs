using Hardcodet.Wpf.TaskbarNotification;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using ReactiveUI;
using ReplayWatcher.Desktop.WindowComponents.Commands;
using ReplayWatcher.Desktop.ViewModel;
using System.Reactive.Disposables;
using System.Windows.Controls.Primitives;

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
            this.Bind(ViewModel, vm => vm.AuthContext.NickName, view => view.RegisterNickNameTextBox.Text)
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
                    ViewModel.AuthContext.Password = RegisterPasswordBox.Password;
                })
                .DisposeWith(disposables);
            RegisterPasswordConfirmBox.Events().PasswordChanged
                .Subscribe(_ =>
                {
                    ViewModel.AuthContext.PasswordConfirm = RegisterPasswordConfirmBox.Password;
                })
                .DisposeWith(disposables);

            this.BindCommand(ViewModel, vm => vm.LoginCommand, view => view.LoginButton)
                .DisposeWith(disposables);
            this.BindCommand(ViewModel, vm => vm.RegisterCommand, view => view.RegisterButton)
                .DisposeWith(disposables);
            //this.BindCommand(ViewModel, vm => vm.GetSecureDataCommand, view => view.GetDataButton)
            //    .DisposeWith(disposables); uncomment for auth debug
            AuthTabs.Events().SelectionChanged
                .Subscribe(_ =>
                {
                    LoginPasswordBox.Clear();
                    RegisterPasswordBox.Clear();
                })
                .DisposeWith(disposables);

            this.BindCommand(ViewModel, vm => vm.SelectPathCommand, view => view.SelectPathButton)
                .DisposeWith(disposables);

            foreach (var command in ViewModel.AllCommands)
            {
                command.ThrownExceptions.Subscribe(HandleError)
                    .DisposeWith(disposables);
            }
        });

        ViewModel!.StartApplicationCommand.Execute();
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

    private void HandleError(Exception e)
    {
        MessageBox.Show(e.Message, "An error has occurred", MessageBoxButton.OK, MessageBoxImage.Error);
    }

    private void Dispose()
    {
        foreach (var disposable in _disposeRequired)
        {
            disposable.Dispose();
        }
    }
}
