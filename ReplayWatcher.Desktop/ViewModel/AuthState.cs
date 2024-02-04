using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ReplayWatcher.Desktop.ViewModel;

public class AuthState : ReactiveObject
{
    [Reactive] public bool IsAuthenticated { get; set; }
}