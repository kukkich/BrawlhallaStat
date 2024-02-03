﻿using System.Reactive;
using Microsoft.Extensions.Logging;
using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReplayWatcher.Desktop.Configuration;
using ReplayWatcher.Desktop.Model.Authentication;
using ReplayWatcher.Desktop.Model.Watcher;

namespace ReplayWatcher.Desktop.ViewModel;

public class AppViewModel : ReactiveObject, IAppViewModel
{
    [Reactive] public bool IsPathInitialized { get; set; }

    [Reactive]
    public string Login
    {
        get; 
        set;
    } = "123";

    [Reactive]
    public string Password
    {
        get;
        set;
    } = "123";
    [Reactive] private bool IsApplicationRunning { get; set; }

    [Reactive]
    public bool IsAuthenticated
    {
        get; 
        set;
    }

    public ReactiveCommand<Unit, Unit> StartApplicationCommand { get; }
    public ReactiveCommand<Unit, AuthenticationResult> LoginCommand { get; set; }

    private readonly ReplayWatcherService _replayWatcher;
    private readonly ConfigurationManager _configurationManager;
    private readonly IAuthService _authService;
    private readonly ILogger<AppViewModel> _logger;

    private AuthState _authState { get; } = new();

    public AppViewModel(
        ReplayWatcherService replayWatcher,
        ConfigurationManager configurationManager,
        IAuthService authService,
        ILogger<AppViewModel> logger)
    {
        _replayWatcher = replayWatcher;
        _configurationManager = configurationManager;
        _authService = authService;
        _logger = logger;

        LoginCommand = ReactiveCommand.CreateFromTask(
            () => LoginAsync(new LoginRequest(Login, Password)),
            this.WhenAnyValue(x => x.IsAuthenticated, x => x.Login, x => x.Password,
                (isAuth, login, password) => 
                    !isAuth && 
                    !String.IsNullOrWhiteSpace(login) &&
                    !String.IsNullOrWhiteSpace(password)
            ));
        LoginCommand.Subscribe(result =>
        {
            _logger.LogInformation("Authenticated");

            _authState.Tokens = result.Tokens;
        });
        _authState.WhenAnyValue(x => x.Tokens, (TokenPair? pair) => pair is not null)
            .Subscribe(pairIsNotNull =>
            {
                _logger.LogDebug("IsAuthenticated changed from {IsAuthenticated} to {NewIsAuth}", IsAuthenticated, pairIsNotNull);
                IsAuthenticated = pairIsNotNull;
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