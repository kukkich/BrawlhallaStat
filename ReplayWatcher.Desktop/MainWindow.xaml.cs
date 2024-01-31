using Hardcodet.Wpf.TaskbarNotification;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace ReplayWatcher.Desktop
{
    public partial class MainWindow : Window
    {
        private readonly TaskbarIcon _taskbarIcon;

        public MainWindow()
        {
            _taskbarIcon = new TaskbarIcon();
            _taskbarIcon.Icon = System.Drawing.SystemIcons.Application;
            _taskbarIcon.ToolTipText = "Replay Watcher";
            _taskbarIcon.DoubleClickCommand = new TreyCommand(ShowWindow); // Добавление обработчика двойного клика
            InitializeComponent();
        }

        protected override void OnInitialized(EventArgs e)
        {
            base.OnInitialized(e);
            CreateContextMenu();
            Hide();
        }

        private void ShowWindow()
        {
            Show();
            WindowState = WindowState.Normal;
        }

        private void Exit()
        {
            _taskbarIcon.Dispose();
            Application.Current.Shutdown();
        }
        
        private void CreateContextMenu()
        {
            _taskbarIcon.ContextMenu = new ContextMenu
            {
                ItemsSource = new MenuItem[]
                {
                    new MenuItem { Header = "Open", Command = new TreyCommand(ShowWindow) },
                    new MenuItem { Header = "Exit", Command = new TreyCommand(Exit) }
                }
            };
        }
        
        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == WindowState.Minimized)
            {
                Hide();
            }
            base.OnStateChanged(e);
        }
        
        protected override void OnClosing(CancelEventArgs e)
        {
            _taskbarIcon.Dispose();
            base.OnClosing(e);
        }
    }
    
    public class TreyCommand : ICommand
    {
        private readonly Action _execute;

        public event EventHandler CanExecuteChanged;

        public TreyCommand(Action execute)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _execute();
        }
    }
}