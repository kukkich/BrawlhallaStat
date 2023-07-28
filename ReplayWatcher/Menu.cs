namespace ReplayWatcher;

public class ConsoleMenu
{
    private readonly MenuAction[] _actions;
    private int _selectedMenuItemIndex = 0;

    public ConsoleMenu(MenuAction[] actions)
    {
        _actions = actions;
    }

    public Task Run(CancellationToken cancellationToken)
    {
        Console.CursorVisible = false;
        ConsoleKey key;

        return Task.Run(() =>
        {
            do
            {
                var returnPosition = Console.GetCursorPosition();
                if (returnPosition is (0, 0))
                {
                    returnPosition = (0, _actions.Length);
                }

                Console.SetCursorPosition(0, 0);
                DrawMenu();

                Console.SetCursorPosition(returnPosition.Left, returnPosition.Top);
                key = Console.ReadKey().Key;


                switch (key)
                {
                    case ConsoleKey.UpArrow:
                        SelectPreviousMenuItem();
                        break;
                    case ConsoleKey.DownArrow:
                        SelectNextMenuItem();
                        break;
                    case ConsoleKey.Enter:
                        _actions[_selectedMenuItemIndex].Action();
                        break;
                }
            } while (key != ConsoleKey.Escape && !cancellationToken.IsCancellationRequested);
        }, CancellationToken.None);
    }

    private void DrawMenu()
    {
        for (var i = 0; i < _actions.Length; i++)
        {
            if (i == _selectedMenuItemIndex)
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("-> " + _actions[i].Name);
                Console.ResetColor();
            }
            else
            {
                Console.WriteLine("   " + _actions[i].Name);
            }
        }
    }

    private void SelectNextMenuItem()
    {
        _selectedMenuItemIndex = (_selectedMenuItemIndex + 1) % _actions.Length;
    }

    private void SelectPreviousMenuItem()
    {
        _selectedMenuItemIndex--;
        if (_selectedMenuItemIndex < 0)
        {
            _selectedMenuItemIndex = _actions.Length - 1;
        }
    }
}