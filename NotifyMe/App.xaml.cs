using System.Windows;
using NotifyMe.Services;
using Application = System.Windows.Application;

namespace NotifyMe
{
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            TrayIconService.Initialize();        
        }
    }
}
