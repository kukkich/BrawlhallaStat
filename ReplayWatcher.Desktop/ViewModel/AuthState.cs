using ReactiveUI;
using ReactiveUI.Fody.Helpers;
using ReplayWatcher.Desktop.Model.Authentication;

namespace ReplayWatcher.Desktop.ViewModel;

public class AuthState : ReactiveObject
{
    [Reactive] public TokenPair? Tokens { get; set; }
}