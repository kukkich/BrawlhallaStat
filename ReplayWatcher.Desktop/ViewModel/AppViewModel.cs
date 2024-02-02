using System.Reactive;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReplayWatcher.Desktop.Configuration;
using ReplayWatcher.Desktop.Model.Authentication;
using ReplayWatcher.Desktop.Model.Watcher;

namespace ReplayWatcher.Desktop.ViewModel;

public class AppViewModel : ReactiveObject, IAppViewModel
{
    [Reactive] public bool IsPathInitialized { get; set; }
    [Reactive] public string Login { get; set; }
    [Reactive] public string Password { get; set; }
    
    public ReactiveCommand<Unit, Unit> StartApplicationCommand { get; }
    public ReactiveCommand<Unit, AuthenticationResult> LoginCommand { get; set; }

    private readonly ReplayWatcherService _replayWatcher;
    private readonly ConfigurationManager _configurationManager;
    private readonly IAuthService _authService;

    [Reactive] private bool IsApplicationRunning { get; set; }
    
    private AuthState _authState { get; } = new();

    public AppViewModel(
        ReplayWatcherService replayWatcher,
        ConfigurationManager configurationManager,
        IAuthService authService)
    {
        _replayWatcher = replayWatcher;
        _configurationManager = configurationManager;
        _authService = authService;

        LoginCommand = ReactiveCommand.CreateFromTask(async () => await LoginAsync(new LoginRequest(Login!, Password!)));
        LoginCommand.Subscribe(result =>
        {
            _authState.Tokens = result.Tokens;
        });

        StartApplicationCommand = ReactiveCommand.CreateFromTask(
            StartApplication,
            this.WhenAnyValue(x => x.IsApplicationRunning)
            );
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

        return Task.CompletedTask;
    }

    private async Task<AuthenticationResult> LoginAsync(LoginRequest request)
    {
        return await _authService.Login(request);
    }
}