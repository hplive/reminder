using System;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Forms;
using Application = System.Windows.Application;

namespace NotifyMe.Services
{
    public static class TrayIconService
    {
        private static NotifyIcon notifyIcon;

        public static void Initialize()
        {
            if (notifyIcon != null)
                return;

            notifyIcon = new NotifyIcon
            {
                Icon = new Icon(@"Assets\app.ico"),    
                Visible = true,
                Text = "NotifyMe - Clique para abrir"
            };

            ContextMenuStrip contextMenu = new ContextMenuStrip();
            contextMenu.Items.Add("Open", null, (s, ev) => ShowMainWindow());     
            contextMenu.Items.Add("Close", null, (s, ev) => ExitApplication());

            notifyIcon.ContextMenuStrip = contextMenu;
            notifyIcon.DoubleClick += (s, ev) => ShowMainWindow();       
        }

        public static void ShowMainWindow()
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                var mainWindow = Application.Current.Windows.OfType<MainWindow>().FirstOrDefault();

                if (mainWindow == null)
                {
                    mainWindow = new MainWindow();
                    Application.Current.MainWindow = mainWindow;
                }
                else
                {
                    if (mainWindow.WindowState == WindowState.Minimized)
                    {
                        mainWindow.WindowState = WindowState.Normal;
                    }

                    mainWindow.Activate();
                    return;
                }

                Rectangle trayArea = GetTrayIconRect();

                double left = trayArea.Left - mainWindow.Width / 2;
                double top = trayArea.Top - mainWindow.Height - 10;

                left = Math.Max(SystemParameters.WorkArea.Left + 10,
                                Math.Min(left, SystemParameters.WorkArea.Right - mainWindow.Width - 10));

                top = Math.Max(SystemParameters.WorkArea.Top + 10,
                               Math.Min(top, SystemParameters.WorkArea.Bottom - mainWindow.Height - 10));

                mainWindow.Left = left;
                mainWindow.Top = top;

                mainWindow.Show();
                mainWindow.Activate();
            });
        }



        private static Rectangle GetTrayIconRect()
        {
            try
            {
                Screen screen = Screen.PrimaryScreen;
                return new Rectangle(screen.WorkingArea.Right - 100, screen.WorkingArea.Bottom - 100, 50, 50);
            }
            catch
            {
                return new Rectangle(100, 100, 50, 50); 
            }
        }

        private static void ExitApplication()
        {
            if (notifyIcon != null)
            {
                notifyIcon.Visible = false;
                notifyIcon.Dispose();
            }

            Application.Current.Shutdown();
        }
    }
}
