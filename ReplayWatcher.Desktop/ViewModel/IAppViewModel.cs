using ReactiveUI;
using ReplayWatcher.Desktop.Model.Authentication;
using System.Reactive;

namespace ReplayWatcher.Desktop.ViewModel;

public interface IAppViewModel
{
    public ReactiveCommand<Unit, Unit> StartApplicationCommand { get; }
    public ReactiveCommand<Unit, AuthenticationResult> LoginCommand { get; }
}