using System.Windows;

namespace NotifyMe.Views
{
    public partial class ReminderPopup : Window
    {
        public ReminderPopup() : this("Lembrete padrão")
        {
        }

        public ReminderPopup(string message)
        {
            InitializeComponent();
            lblMessage.Text = message;
        }

        private void OkButton_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
