using NotifyMe.Views;
using System.Windows;

namespace NotifyMe
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            this.Closing += MainWindow_Closing;
        }

        private void AddReminder_Click(object sender, RoutedEventArgs e)
        {
            ReminderPopup popup = new ReminderPopup();
            popup.ShowDialog();
        }

        private void RemoveReminder_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.MessageBox.Show("Funcionalidade em desenvolvimento.");
        }

        private void OpenSettings_Click(object sender, RoutedEventArgs e)
        {
            SettingsWindow settings = new SettingsWindow();
            settings.ShowDialog();
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;

            this.Hide();
        }




    }
}
