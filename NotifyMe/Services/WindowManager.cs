using System.Linq;
using System.Windows;
using Application = System.Windows.Application;

namespace NotifyMe.Services
{
    public static class WindowManager
    {
        public static void ShowMainWindow()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault() ?? new MainWindow();

                mainWindow.Left = SystemParameters.WorkArea.Right - mainWindow.Width - 10;
                mainWindow.Top = SystemParameters.WorkArea.Bottom - mainWindow.Height - 10;

                mainWindow.Show();
                mainWindow.Activate();
            });
        }
    }
}
