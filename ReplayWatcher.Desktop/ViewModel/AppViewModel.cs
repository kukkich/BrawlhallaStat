using System.IO;
using System.Net.Http;
using System.Reactive;
using System.Reactive.Linq;
using System.Windows.Forms;
using Microsoft.Extensions.Logging;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReplayWatcher.Desktop.Configuration;
using ReplayWatcher.Desktop.Model.Authentication;
using ReplayWatcher.Desktop.Model.Authentication.Services;
using ReplayWatcher.Desktop.Model.ReplayService;
using ReplayWatcher.Desktop.Model.Watcher;
using ReplayWatcher.Desktop.ViewModel.Auth;
using FileDialogResult = System.Windows.Forms.DialogResult;
using MessageBox = System.Windows.MessageBox;

namespace ReplayWatcher.Desktop.ViewModel;

public class AppViewModel : ReactiveObject
{
    [Reactive] public bool IsPathInitialized { get; private set; }
    [Reactive] public bool IsApplicationRunning { get; private set; }
    [Reactive] public AuthContext AuthContext { get; private set; } = new();

    public ReactiveCommand<Unit, Unit> StartApplicationCommand { get; private set; }
    public ReactiveCommand<Unit, AuthenticationResult> LoginCommand { get; private set; }
    public ReactiveCommand<Unit, AuthenticationResult> RegisterCommand { get; private set; }
    public ReactiveCommand<Unit, Unit> GetSecureDataCommand { get; private set; }
    public ReactiveCommand<Unit, Unit> SelectPathCommand { get; private set; }
    public ReactiveCommand<string, Unit> UploadReplayCommand { get; private set; }

    public readonly IObservable<EventPattern<FileSystemEventArgs>> ReplayCreatedObservable;

    private readonly ReplayWatcherService _replayWatcher;
    private readonly ConfigurationManager _configurationManager;
    private readonly IAuthService _authService;
    private readonly ILogger<AppViewModel> _logger;
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly IReplayService _replayService;
    
    public AppViewModel(
        ReplayWatcherService replayWatcher,
        ConfigurationManager configurationManager,
        IAuthService authService,
        ILogger<AppViewModel> logger,
        IHttpClientFactory httpClientFactory,
        IReplayService replayService)
    {
        _replayWatcher = replayWatcher;
        _configurationManager = configurationManager;
        _authService = authService;
        _logger = logger;
        _httpClientFactory = httpClientFactory;
        _replayService = replayService;

        ConfigureAuthentication();
        StartApplicationCommand = ReactiveCommand.CreateFromTask(StartApplication);
        SelectPathCommand = ReactiveCommand.Create(SelectReplaysDirectory);

        UploadReplayCommand = ReactiveCommand.CreateFromTask<string>(async path =>
        {
            logger.LogInformation("File created: {path}", path);

            await _replayService.Upload(path);
        });

        Observable.FromEventPattern<FileSystemEventHandler, FileSystemEventArgs>(
                handler => _replayWatcher.FileCreated += handler,
                handler => _replayWatcher.FileCreated -= handler
            )
            .Subscribe(pattern => UploadReplayCommand.Execute(pattern.EventArgs.FullPath).Subscribe());
    }

    private void ConfigureAuthentication()
    {
        LoginCommand = ReactiveCommand.CreateFromTask(
            () => LoginAsync(new LoginRequest(AuthContext.Login, AuthContext.Password)),
            this.WhenAnyValue(x => x.AuthContext.IsAuthenticated, x => x.AuthContext.Login, x => x.AuthContext.Password,
                (isAuth, login, password) =>
                    !isAuth &&
                    !String.IsNullOrWhiteSpace(login) &&
                    !String.IsNullOrWhiteSpace(password)
            ));
        LoginCommand.Subscribe(result =>
        {
            _logger.LogInformation("Authenticated");

            if (!result.IsSucceed)
            {
                foreach (var error in result.Errors!)
                {
                    _logger.LogInformation("Error while login: {message}", error);
                }
            }

            AuthContext.IsAuthenticated = result.IsSucceed;
        });
        RegisterCommand = ReactiveCommand.CreateFromTask(
            () => RegisterAsync(new RegisterRequest(AuthContext.Login, AuthContext.Email, AuthContext.Password)),
            this.WhenAnyValue(x => x.AuthContext.IsAuthenticated,
                x => x.AuthContext.Email, x => x.AuthContext.Login, x => x.AuthContext.Password,
                (isAuth, login, email, password) =>
                    !isAuth &&
                    !String.IsNullOrWhiteSpace(login) &&
                    !String.IsNullOrWhiteSpace(email) &&
                    !String.IsNullOrWhiteSpace(password)
            ));
        RegisterCommand.Subscribe(result =>
        {
            _logger.LogInformation("Registered");

            if (!result.IsSucceed)
            {
                foreach (var error in result.Errors!)
                {
                    _logger.LogInformation("Error while register: {message}", error);
                }
            }

            AuthContext.IsAuthenticated = result.IsSucceed;
        });

        GetSecureDataCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var client = _httpClientFactory.CreateClient("GeneralApiClient");
            var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "api/Secret/SimpleSecret"));

            MessageBox.Show(await response.Content.ReadAsStringAsync());
        });
    }

    public async Task StartApplication()
    {
        var refreshResult = await _authService.RefreshToken();
        AuthContext.IsAuthenticated = refreshResult.IsSucceed;

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

    private void SelectReplaysDirectory()
    {
        using var dialog = new FolderBrowserDialog();

        dialog.Description = "Select replays directory";

        if (dialog.ShowDialog() is not FileDialogResult.OK)
        {
            return;
        }

        var path = dialog.SelectedPath;

        _configurationManager.Configuration.WatcherDirectory = path;
        _configurationManager.Save();
    }

    private async Task<AuthenticationResult> LoginAsync(LoginRequest request)
    {
        return await _authService.Login(request);
    }

    private async Task<AuthenticationResult> RegisterAsync(RegisterRequest request)
    {
        return await _authService.Register(request);
    }
}