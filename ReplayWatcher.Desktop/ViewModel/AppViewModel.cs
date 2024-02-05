using System.Net.Http;
using System.Reactive;
using System.Windows;
using Microsoft.Extensions.Logging;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReplayWatcher.Desktop.Configuration;
using ReplayWatcher.Desktop.Model.Authentication;
using ReplayWatcher.Desktop.Model.Authentication.Services;
using ReplayWatcher.Desktop.Model.Watcher;
using ReplayWatcher.Desktop.ViewModel.Auth;

namespace ReplayWatcher.Desktop.ViewModel;

public class AppViewModel : ReactiveObject
{
    [Reactive] public bool IsPathInitialized { get; set; }
    [Reactive] private bool IsApplicationRunning { get; set; }
    [Reactive] public AuthContext AuthContext { get; set; } = new();

    public ReactiveCommand<Unit, Unit> StartApplicationCommand { get; }
    public ReactiveCommand<Unit, AuthenticationResult> LoginCommand { get; set; }
    public ReactiveCommand<Unit, AuthenticationResult> RegisterCommand { get; set; }
    public ReactiveCommand<Unit, Unit> GetSecureDataCommand { get; set; }

    private readonly ReplayWatcherService _replayWatcher;
    private readonly ConfigurationManager _configurationManager;
    private readonly IAuthService _authService;
    private readonly ILogger<AppViewModel> _logger;
    private readonly IHttpClientFactory _httpClientFactory;


    public AppViewModel(
        ReplayWatcherService replayWatcher,
        ConfigurationManager configurationManager,
        IAuthService authService,
        ILogger<AppViewModel> logger, 
        IHttpClientFactory httpClientFactory)
    {
        _replayWatcher = replayWatcher;
        _configurationManager = configurationManager;
        _authService = authService;
        _logger = logger;
        _httpClientFactory = httpClientFactory;

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
                x=> x.AuthContext.Email, x => x.AuthContext.Login, x => x.AuthContext.Password,
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

        StartApplicationCommand = ReactiveCommand.CreateFromTask(
            StartApplication,
            this.WhenAnyValue(x => x.IsApplicationRunning, value => !value)
        );

        GetSecureDataCommand = ReactiveCommand.CreateFromTask(async () =>
        {
            var client = _httpClientFactory.CreateClient("GeneralApiClient");
            var response = await client.SendAsync(new HttpRequestMessage(HttpMethod.Get, "api/Secret/SimpleSecret"));

            MessageBox.Show(await response.Content.ReadAsStringAsync());
        });
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

    private async Task<AuthenticationResult> RegisterAsync(RegisterRequest request)
    {
        return await _authService.Register(request);
    }
}