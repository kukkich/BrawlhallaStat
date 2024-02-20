using ReactiveUI;
using ReactiveUI.Fody.Helpers;

namespace ReplayWatcher.Desktop.ViewModel.Auth;

public class AuthContext : ReactiveObject
{
    [Reactive] public bool IsAuthenticated { get; set; }
    [Reactive] public string Login { get; set; } = null!;
    [Reactive] public string NickName { get; set; } = null!;
    [Reactive] public string Email { get; set; } = null!;
    [Reactive] public string Password { get; set; } = null!;
    [Reactive] public string PasswordConfirm { get; set; } = null!;
}
