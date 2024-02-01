using System.Windows.Input;

namespace ReplayWatcher.Desktop.WindowComponents.Commands;

public class ActionCommand : ICommand
{
    private readonly Action _execute;

    public event EventHandler? CanExecuteChanged;

    public ActionCommand(Action execute)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
    }

    public bool CanExecute(object? _) => true;

    public void Execute(object? parameter)
    {
        _execute();
    }
}