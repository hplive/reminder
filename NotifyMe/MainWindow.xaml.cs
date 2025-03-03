using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Threading;
using Newtonsoft.Json;
using NotifyMe.Services;
using NotifyMe.Views;
using Application = System.Windows.Application;
using MessageBox = System.Windows.MessageBox;

namespace NotifyMe
{
    public partial class MainWindow : Window
    {
        private DispatcherTimer reminderTimer;

        public MainWindow()
        {
            InitializeComponent();
            this.Closing += MainWindow_Closing;
            LoadReminders();
            LoadRecentReminders();
            IniciarContadorGlobal();
        }

        private void MainWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Hide();
            Application.Current.MainWindow = null;      
        }

        private void IniciarContadorGlobal()
        {
            reminderTimer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(1)
            };
            reminderTimer.Tick += ReminderTimer_Tick;
            reminderTimer.Start();
        }

        private void ReminderTimer_Tick(object sender, EventArgs e)
        {
            var reminders = ReminderStorage.LoadReminders().OrderBy(r => r.ProximoLembrete).ToList();
            if (reminders.Any())
            {
                var nextReminder = reminders.First();
                TimeSpan restante = nextReminder.ProximoLembrete - DateTime.Now;

                if (restante.TotalSeconds <= 0)
                {
                    if (nextReminder.ProximoLembrete > DateTime.Now)
                        return;

                    nextReminder.ProximoLembrete = DateTime.Now.AddMinutes(nextReminder.IntervaloMinutos);
                    File.WriteAllText("reminders.json", JsonConvert.SerializeObject(reminders));

                    MessageBox.Show($"Reminder: {nextReminder.Type} - {nextReminder.Details}",
                                    $"{nextReminder.Type} Reminder", MessageBoxButton.OK, MessageBoxImage.Information);
                }

                Title = $"Next Reminder: {restante.Minutes}m {restante.Seconds}s";
            }
        }




        private void OpenStretchReminder(object sender, RoutedEventArgs e)
        {
            StretchReminderForm stretchReminder = new StretchReminderForm();
            stretchReminder.ShowDialog();
        }

        private void OpenMyopiaReminder(object sender, RoutedEventArgs e)
        {
            MyopiaReminderForm myopiaReminder = new MyopiaReminderForm();
            myopiaReminder.ShowDialog();
        }
        private void OpenWaterReminder(object sender, RoutedEventArgs e)
        {
            WaterReminderForm waterReminder = new WaterReminderForm();
            waterReminder.ShowDialog();
        }


        public void LoadRecentReminders()
        {
            lstRecentReminders.Items.Clear();
            var reminders = ReminderStorage.LoadReminders();

            foreach (var reminder in reminders.OrderBy(r => r.ProximoLembrete).Take(3))
            {
                double litros = 2.0;
                double copoMl = 250;
                double copoLitros = copoMl / 1000.0;
                int totalCopos = (int)(litros / copoLitros);
                DateTime endTime = reminder.Date.AddMinutes(reminder.IntervaloMinutos * totalCopos);

                lstRecentReminders.Items.Add(
                    $"{reminder.Type} - Start: {reminder.Date:HH:mm}, End: {endTime:HH:mm}, Every {reminder.IntervaloMinutos} min"
                );
            }
        }







    }
}
